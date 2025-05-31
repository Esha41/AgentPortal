
using AutoMapper;
using Intelli.AgentPortal.Api.DTO;
using Intelli.AgentPortal.Api.Helpers;
using Intelli.AgentPortal.Api.Services.Session;
using Intelli.AgentPortal.Auth.Api.Models;
using Intelli.AgentPortal.Domain.Core.Helpers;
using Intelli.AgentPortal.Domain.Core.Repository;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.EventBus.RabbitMQ.Event;
using Intelli.AgentPortal.EventBus.RabbitMQ.Sender;
using Intelli.AgentPortal.Shared.Mvc;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Services
{
    /// <summary>
    /// The auth service implementation
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly AgentPortalSignInManager _signInManager;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;
        private readonly IAuthEmailService _emailService;
        private readonly IRepository<SystemUser> _userRepository;
        private readonly IRepository<SystemUserRole> _userRoleRepository;
        private readonly IRepository<SystemUserCountry> _userCountryRepository;
        private readonly IRepository<UserCompany> _userCompanyRepository;
        private readonly AgentPortalContext _context;
        private readonly string _userName;
        private readonly List<int> _CompanyIds;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="signInManager">User sign in manager</param>
        /// <param name="userManager">The identity user manager.</param>
        /// <param name="context">The authorization database context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="sender">The Event Sender.</param>
        /// <param name="httpContext">The current Http context.</param>
        /// <param name="logger">Logs information and errors.</param>
        /// <param name="emailService">The auth email service.</param>
        public AuthService(AgentPortalSignInManager signInManager,
            UserManager<AspNetUser> userManager,
            AgentPortalContext context,
            IMapper mapper,
            IEventSender sender,
            IHttpContextAccessor httpContext,
            ILogger<AuthService> logger,

            IAuthEmailService emailService)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
            _userManager = userManager;

            _userRepository = new GenericRepository<SystemUser>(context);
            _userRoleRepository = new GenericRepository<SystemUserRole>(context);
            _userCountryRepository = new GenericRepository<SystemUserCountry>(context);
            _userCompanyRepository = new GenericRepository<UserCompany>(context);

            ((GenericRepository<SystemUserCountry>)_userCountryRepository).AfterSave =
            ((GenericRepository<SystemUserRole>)_userRoleRepository).AfterSave =
            ((GenericRepository<UserCompany>)_userCompanyRepository).AfterSave =
            ((GenericRepository<SystemUser>)_userRepository).AfterSave = (logs) =>
            {
                sender.SendEvent(new MQEvent<List<AuditEntry>>(Shared.Mvc.Controllers.BaseController.AUDIT_EVENT_NAME, (List<AuditEntry>)logs));
            };

            _context = context;
            _userName = httpContext.HttpContext.GetUserEmail();
            _CompanyIds = httpContext.HttpContext.GetCompanyIds();
            
        }
        /// <summary>
        /// Validates the user in the identity store
        /// </summary>
        /// <param name="loginDTO">The loginDTO.</param>
        /// <param name="forceSignIn">If true, it is used to signIn user regardless its session is already active.</param>
        /// <returns>Identity User</returns>
        public async Task<UserLoginResult> ValidateUser(LoginDTO loginDTO, bool forceSignIn)
        {
            var loginResult = new UserLoginResult() { IdentityResult = IdentityResult.Failed() };
            try
            {
                // Finding user by email from the identity store.
                var identityUser = await _userManager.FindByEmailAsync(loginDTO.Email);

                // If user not found username is incorrect.
                if (identityUser == null)
                {
                    loginResult.Message = MsgKeys.UsernameIsIncorrect;
                }
                else
                {
                    _signInManager.ForceSignIn = forceSignIn;
                    var signInResult = await _signInManager.PasswordSignInAsync(identityUser, loginDTO.Password, false, false);
                    if (signInResult.Succeeded)
                    {
                        loginResult.IdentityResult = IdentityResult.Success;
                        loginResult.User = identityUser.SystemUser;
                    }
                    else if (signInResult.IsNotAllowed)
                    {
                        loginResult.Message = MsgKeys.EmailNotVerified;
                    }
                    else
                    {
                        loginResult.Message = MsgKeys.PasswordIsIncorrect;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot login user with following Email:{0}", loginDTO.Email);
                loginResult.Message = ex.Message;
            }
            return loginResult;
        }

        /// <summary>
        /// Registers the user in the identity store
        /// </summary>
        /// <param name="registrationDTO">The registrationDTO to encapsulate username, password, email.</param>
        /// <returns>Identity result created by the user manager</returns>
        public async Task<UserRegistrationResult> RegisterUser(UserReadDTO registrationDTO)
        {
            var result = new UserRegistrationResult() { IdentityResult = IdentityResult.Failed() };

            await using (var transaction = _userRepository.GetTransaction())
            {
                try
                {
                    var user = _mapper.Map<SystemUser>(registrationDTO);
                    _userRepository.Insert(user);
                    _userRepository.SaveChanges(_userName, transaction);
                    result.User = user;

                    var identityUser = new AspNetUser()
                    {
                        UserName = registrationDTO.Email,
                        Email = registrationDTO.Email,
                        SystemUserId = user.Id,
                        Enabled=false,
                        CreatedAt=DateTime.Now
                    };

                    var passwordOptions = ConfigurationHelper.Read(_context, _mapper).ToPasswordOptions();
                    string password = DefaultPasswordGenerator.GenerateRandomPassword(passwordOptions);
                    result.IdentityResult = await _userManager.CreateAsync(identityUser, password);
                    

                    if (result.IdentityResult.Succeeded)
                    {
                        // Add user roles
                        AddUserRelatedData(user.Id, registrationDTO, transaction);

                        // Set Default Preferences
                        await new PreferencesHelper(_context).SetDefaultAsync(user.Id, _userName);

                        // Commit transaction
                        await transaction.CommitAsync();

                        // Send Confirmation email
                        await SendEmailConfirmation(identityUser);
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                    }
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "User registration failed with data {0}", registrationDTO);

                    result.Error = new Exception(MsgKeys.UserAlreadyExists);
                    result.Message = MsgKeys.UserDuplicated;

                    await transaction.RollbackAsync();
                }
                catch (Exception ex)
                {
                    result.Error = ex;
                    await transaction.RollbackAsync();
                }
            }

            // Create the user in the identity store
            return result;
        }

        /// <summary>
        /// Adds the user roles and user groups after removing existing roles and groups.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="dto">The User read dto.</param>
        /// <param name="transaction">The database transaction.</param>
        private void AddUserRelatedData(int userId, UserReadDTO dto, ITransactionHandler transaction)
        {
            // Update user roles
            _userRoleRepository.Delete(x => x.SystemUserId == userId);

            if (dto.RoleIds != null && dto.RoleIds.Count > 0)
                foreach (int roleId in dto.RoleIds.Distinct().OrderBy(x => x))
                    _userRoleRepository.Insert(new SystemUserRole { SystemUserId = userId, SystemRoleId = roleId });

            _userRoleRepository.SaveChanges(_userName, transaction);

            // Update user countries
            _userCountryRepository.Delete(x => x.SystemUserId == userId);

            if (dto.CountryIds != null && dto.CountryIds.Count > 0)
                foreach (int countryId in dto.CountryIds.Distinct().OrderBy(x => x))
                    _userCountryRepository.Insert(new SystemUserCountry { SystemUserId = userId, CountryId = countryId });

            _userCountryRepository.SaveChanges(_userName, transaction);
            
            
            //update the user companies
            _userCompanyRepository.Delete(x => x.SystemUserId == userId);

            if (dto.CompanyIds != null && dto.CompanyIds.Count > 0)
                foreach (int companyId in dto.CompanyIds.Distinct().OrderBy(x => x))
                    _userCompanyRepository.Insert(new UserCompany { CompanyId = companyId, SystemUserId = userId });

            _userCompanyRepository.SaveChanges(_userName, transaction);
        }

        /// <summary>
        /// Verifies the email.
        /// </summary>
        /// <param name="dto">The verify email dto.</param>
        /// <returns>A Task of UserRegistrationResult.</returns>
        public async Task<UserLoginResult> VerifyEmail(VerifyEmailDTO dto)
        {
            var result = new UserLoginResult()
            {
                IdentityResult = IdentityResult.Failed(),
                Message = "Invalid Token."
            };

            await using (var transaction = _userRepository.GetTransaction())
            {
                try
                {
                    var authUser = await _userManager.FindByIdAsync(dto.UserId);
                    if (authUser == null) return result;

                    // Verify code
                    string code = Encoding.Default.GetString(Convert.FromBase64String(dto.Code));
                    if (string.Compare(code, authUser.Email, true) != 0) return result;

                    // Verify token
                    string token = Encoding.Default.GetString(Convert.FromBase64String(dto.Token));

                    if (dto.Flag == 0)
                    {
                        result.IdentityResult = await _userManager.ConfirmEmailAsync(authUser, token);
                    }
                    else
                    {
                        var passwordOptions = ConfigurationHelper.Read(_context, _mapper).ToPasswordOptions();
                        var password = DefaultPasswordGenerator.GenerateRandomPassword(passwordOptions);
                        result.IdentityResult = await _userManager.ResetPasswordAsync(authUser, token, password);
                    }

                    if (result.IdentityResult.Succeeded)
                    {
                        // Get user from repository
                        var userData = await _userRepository.Query(x => x.Id == authUser.SystemUserId).Include(x => x.UserCompanies).FirstOrDefaultAsync();

                        
                        result.User = authUser.SystemUser = userData;

                        // Perform validations
                        await _signInManager.PerformValidations(authUser);

                        await transaction.CommitAsync();
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                    }
                }
                catch (Exception e)
                {
                    result.Error = e;
                    result.Message = MsgStrings.GetMsg(e.Message);

                    await transaction.RollbackAsync();
                }
            }
            return result;
        }

        /// <summary>
        /// Registers the user in the identity store
        /// </summary>
        /// <param name="dto">The user read dto to update user</param>
        /// <returns>Identity result created by the user manager</returns>
        public async Task<UserRegistrationResult> UpdateUser(UserReadDTO dto)
        {
            var result = new UserRegistrationResult() { IdentityResult = IdentityResult.Failed() };

            await using var transaction = _userRepository.GetTransaction();
            try
            {
                string oldEmail;
                string oldPhone;

                var identityUser = await _context.AspNetUsers.FirstOrDefaultAsync(_ => _.SystemUserId == dto.Id);
                if (identityUser != null)
                {
                    // Save old email to check if email is changed or not
                    oldEmail = identityUser.Email;

                    // Check if email changed and needs to be confirmed
                    if (string.Compare(oldEmail, dto.Email, true) != 0)
                    {
                        // Change username
                        identityUser.UserName = dto.Email;

                        // Change email
                        identityUser.Email = dto.Email;

                        // If email changed force re-confirmation
                        identityUser.EmailConfirmed = false;
                    }

                    // Save old phone to check if phone is changed or not
                    oldPhone = identityUser.PhoneNumber ?? string.Empty;

                    // Update identity user
                    result.IdentityResult = await _userManager.UpdateAsync(identityUser);
                }
                else
                    throw new Exception(MsgKeys.ObjectNotExists);

                if (result.IdentityResult.Succeeded)
                {
                    var user = _mapper.Map<SystemUser>(dto);
                    _userRepository.Update(user);
                    _userRepository.SaveChanges(_userName, transaction);

                    // Add user roles
                    AddUserRelatedData(user.Id, dto, transaction);

                    // commit transaction
                    await transaction.CommitAsync();

                    result.User = user;

                    // Check if email changed and needs to be confirmed
                    if (string.Compare(oldEmail, dto.Email, true) != 0)
                        await SendEmailConfirmation(identityUser);
                }
                else
                {
                    // Show error messgae
                    result.Message = MsgKeys.UserDuplicated;

                    // rollback transaction
                    await transaction.RollbackAsync();
                }
            }
            catch (DbUpdateException ex)
            {
                // rollback transaction
                await transaction.RollbackAsync();

                _logger.LogError(ex, "User updation failed with data {0}", dto);

                result.Error = ex;
                result.Message = MsgKeys.UserDuplicated;
            }
            catch (Exception e)
            {
                // rollback transaction
                await transaction.RollbackAsync();

                result.Error = e;
            }
            return result;
        }

        /// <summary>
        /// Updates the user credentials.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>A Task.</returns>
        public async Task<UserRegistrationResult> UpdateUserCredentials(UserCredentialsDTO dto)
        {
            var result = new UserRegistrationResult() { IdentityResult = IdentityResult.Failed() };
            try
            {
                var identityUser = await _context.AspNetUsers.FirstOrDefaultAsync(_ => _.SystemUserId == dto.Id);
                if (identityUser != null)
                {
                    // Validate password
                    foreach (var pwdValidator in _userManager.PasswordValidators)
                    {
                        result.IdentityResult = await pwdValidator.ValidateAsync(_userManager, identityUser, dto.Password);
                        if (!result.IdentityResult.Succeeded) return result;
                    }

                    // Change password
                    identityUser.PasswordHash = _userManager.PasswordHasher.HashPassword(identityUser, dto.Password);
                    await using var trans = _userRepository.GetTransaction();
                    try
                    {
                        // Update identity user
                        result.IdentityResult = await _userManager.UpdateAsync(identityUser);

                        // Log password hash
                        await _context.PasswordHistories.AddAsync(new PasswordHistory
                        {
                            SystemUserId = dto.Id,
                            ChangedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                            PasswordHash = await StringHasher.GetHashAsync(dto.Password),
                        });

                        // Save changes
                        await _context.SaveChangesAsync();

                        // Commit transaction
                        await trans.CommitAsync();
                    }
                    catch (Exception e)
                    {
                        // Rollback transaction
                        await trans.RollbackAsync();

                        // Log error message
                        _logger.LogError(e, "{0}", e.Message);
                    }
                }
                else
                {
                    throw new Exception(MsgKeys.ObjectNotExists);
                }
            }
            catch (Exception e)
            {
                result.Error = e;
            }
            return result;
        }

        /// <summary>
        /// Resends the email confirmation.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>True if email send successfully, else returns false.</returns>
        public async Task<bool> ResendEmailConfirmation(int userId)
        {
            try
            {
                var identityUser = await _context.AspNetUsers.FirstOrDefaultAsync(_ => _.SystemUserId == userId);
                if (identityUser != null)
                {
                    await SendEmailConfirmation(identityUser);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Resend Email confirmation failure occurs for UserId:{0}", userId);
                throw;
            }
            return false;
        }

        /// <summary>
        /// Sends the email confirmation.
        /// </summary>
        /// <param name="identityUser">The identity user.</param>
        /// <returns>A Task.</returns>
        private async Task SendEmailConfirmation(AspNetUser identityUser)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
            await _emailService.SendEmailConfirmation(identityUser.Id.ToString(), token, identityUser.Email);
        }

        /// <summary>
        /// Sends the forgot password link to user's email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>Empty string if email sent successfully, else returns error message.</returns>
        public async Task<string> SendPasswordResetLink(string email)
        {
            try
            {
                var identityUser = await _context.AspNetUsers.FirstOrDefaultAsync(_ => _.Email == email);
                if (identityUser != null)
                {
                    if (identityUser.EmailConfirmed)
                    {
                        await SendPasswordResetLink(identityUser);
                        return string.Empty;
                    }
                    else
                        return MsgKeys.EmailNotVerified;
                }
                return MsgKeys.EmailNotFound;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Send Password resent link failure occurs for Email:{0}", email);

                return MsgKeys.UnknownFailure;
            }
        }

        /// <summary>
        /// Sends the password reset link.
        /// </summary>
        /// <param name="identityUser">The identity user.</param>
        /// <returns>A Task.</returns>
        private async Task SendPasswordResetLink(AspNetUser identityUser)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(identityUser);
            await _emailService.SendPasswordResetLink(identityUser.Id.ToString(), token, identityUser.Email);
        }

        public List<int> GetAssociatedCompaniesId( int userId)
        {
            var query = _userCompanyRepository.Query(x => x.SystemUserId == userId);

            return query.Select(x => x.CompanyId).ToList();
        }
    }
}