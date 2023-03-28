using AutoMapper;
using Intelli.AgentPortal.Api.Services.Session;
using Intelli.AgentPortal.Domain;
using Intelli.AgentPortal.Domain.Core.Repository;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Services
{
    /// <summary>
    /// The agent portal sign in manager.
    /// Agent portal sign in manager is responsible for signing in the user in portal.
    /// Before signing in it performs following validations:
    /// 1. Checks if user is active.
    /// 2. Checks if users per company limit is exceeded.
    /// 3. Checks if user is already logged in.
    /// </summary>
    public class AgentPortalSignInManager : SignInManager<AspNetUser>
    {
        private readonly IRepository<SystemUser> _userRepository;
        private readonly IRepository<Company> _companyRepository;
        private readonly ISessionManager _sessionManager;

        /// <summary>
        /// Gets or sets a value indicating whether force sign in.
        /// Regardless of users' another session is active.
        /// </summary>
        public bool ForceSignIn { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentPortalSignInManager"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="contextAccessor">The context accessor.</param>
        /// <param name="claimsFactory">The claims factory.</param>
        /// <param name="optionsAccessor">The options accessor.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        /// <param name="schemes">The schemes.</param>
        /// <param name="userConfirmation">The user confirmation.</param>
        public AgentPortalSignInManager(UserManager<AspNetUser> userManager,
                                            IHttpContextAccessor contextAccessor,
                                            IUserClaimsPrincipalFactory<AspNetUser> claimsFactory,
                                            IOptions<IdentityOptions> optionsAccessor,
                                            ILogger<AgentPortalSignInManager> logger,
                                            AgentPortalContext context,
                                            IAuthenticationSchemeProvider schemes,
                                            IUserConfirmation<AspNetUser> userConfirmation,
                                            IConfiguration configuration,
                                            ISessionManager sessionManager)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, userConfirmation)
        {
            _userRepository = new GenericRepository<SystemUser>(context);
            _companyRepository = new GenericRepository<Company>(context);
            _sessionManager = sessionManager;
        }

        /// <summary>
        /// Performs signin with password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="isPersistent">If true, is persistent.</param>
        /// <param name="lockoutOnFailure">If true, lockout on failure.</param>
        /// <returns>A SignInResult.</returns>
        public override async Task<SignInResult> PasswordSignInAsync(AspNetUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var result = await base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
            if (result.Succeeded)
            {

                // Get user from repository
                var userData = await  _userRepository.Query(x => x.Id == user.SystemUserId).Include(x => x.UserCompanies).FirstOrDefaultAsync();
                user.SystemUser = userData;

                // Perform Validations
                await PerformValidations(user);
            }
            return result;
        }

        /// <summary>
        /// Performs the validations.
        /// </summary>
        /// <param name="user">The AspNetUser user.</param>
        /// <returns>A Task.</returns>
        public async Task PerformValidations(AspNetUser user)
        {
            // Check if user is active or not
            if (user.SystemUser == null || !(user.SystemUser.IsActive ?? false))
                throw new Exception(MsgKeys.UserDeactivated);

            //Validate Users Per Company
            if (user.SystemUser.UserCompanies != null && user.SystemUser.UserCompanies.Any())
            {
                var totalCompanies = user.SystemUser.UserCompanies.Count();
                int counter = 0;
                foreach (var systemUser in user.SystemUser.UserCompanies)
                {
                    // Get users per company limit
                    var usersPerCompany = await GetUsersPerCompanyAsync(systemUser.CompanyId);

                    // Get logged in users
                    var loggedInUsers = await GetLoggedInUsersAsync(systemUser.CompanyId, user.SystemUserId);

                    //Check if user per company limit exceeded.
                    if (loggedInUsers > usersPerCompany)
                    {
                        counter += 1;
                        continue;
                    }

                    break;

                }

                if(counter == totalCompanies)
                {
                    throw new Exception(MsgKeys.SignInLimitExceeded);
                }
            }


            // Check user already signed in
            if (!ForceSignIn && await _sessionManager.IsActive(user.SystemUserId))
                throw new Exception(MsgKeys.UserAlreadySignedIn);
        }

        /// <summary>
        /// Gets the allowed users per company.
        /// </summary>
        /// <param name="companyId">The System User's company id.</param>
        /// <returns>An int.</returns>
        private async Task<int> GetUsersPerCompanyAsync(int companyId)
        {
            var query = _companyRepository.Query(x => x.Id == companyId);
            var entity = await query.FirstOrDefaultAsync();
            return entity != null ? EncryptionHelper.DecryptUsersPerCompany(entity.UsersPerCompany) : 0;
        }

        /// <summary>
        /// Gets the current logged in users of the company.
        /// Excluding current users attempting to sign in.
        /// </summary>
        /// <param name="companyId">The System User's company id.</param>
        /// <param name="userId">Current user's id attempting to sign in.</param>
        /// <returns>A Task.</returns>
        private async Task<int> GetLoggedInUsersAsync(int companyId, int userId)
        {
            var query = _userRepository.Query(x => x.UserCompanies.Where(x=>x.Company.Id == companyId).FirstOrDefault().Company.Id == companyId && x.Id != userId)
                                        .Include(x => x.UserSession);

            var list = await query.ToListAsync();

            int loggedInUsers = list.Where(x => x.UserSession != null).Count();

            return loggedInUsers;
        }
    }
}
