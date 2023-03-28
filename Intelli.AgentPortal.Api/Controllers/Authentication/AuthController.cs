using AutoMapper;
using Intelli.AgentPortal.Api.DTO;
using Intelli.AgentPortal.Api.Helpers;
using Intelli.AgentPortal.Api.Services;
using Intelli.AgentPortal.Api.Services.Session;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Controllers.v1
{
    /// <summary>
    /// The authentication / authorization controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly IPrivilegesService _privilegesService;
        private readonly IJWTService _tokenService;
        private readonly AgentPortalContext _context;
        private readonly IMapper _mapper;
        private readonly ISessionManager _sessionManager;
        private readonly int ForcePasswordChangeDays;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">The auth service that will in injected via constructor IOC</param>
        /// <param name="logger">The logger.</param>
        /// <param name="privilegesService">The privileges service.</param>
        /// <param name="tokenService">The json web token service.</param>
        /// <param name="mapper">The auto mapper.</param>
        /// <param name="sessionManager">The session manager.</param>
        public AuthController(IAuthService authService,
            ILogger<AuthController> logger,
            IPrivilegesService privilegesService,
            IJWTService tokenService,
            AgentPortalContext context,
            IMapper mapper,
            ISessionManager sessionManager)
        {
            _authService = authService;
            _logger = logger;
            _privilegesService = privilegesService;
            _tokenService = tokenService;
            _context = context;
            _mapper = mapper;
            _sessionManager = sessionManager;

            var dto = ConfigurationHelper.Read(context, mapper);
            ForcePasswordChangeDays = dto.ForcePasswordChangeDays;
        }

        /// <summary>
        /// Authenticate the user and generate the JWT token.
        /// </summary>
        /// <returns>Async ActionResult (JSON)</returns>
        /// <response code="200">JWT token and user Object</response>
        /// <response code="404">Error in case the user was not found</response>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto, [FromQuery] bool forceSignIn)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest(MsgKeys.InvalidLoginCredentials);
            }

            // Validating if the passed data corresponds to a valid user
            var result = await _authService.ValidateUser(dto, forceSignIn);

            if (result.Error != null)
            {
                return BadRequest(new { result.Message, Errors = result.Error });
            }

            // If verification failed
            if (!result.IdentityResult.Succeeded)
            {
                Dictionary<string, string> dictionary = new();

                // Collect all errors in a dictionary
                foreach (var error in result.IdentityResult.Errors)
                {
                    dictionary[error.Code] = error.Description;
                }

                // Passing the errors dictionary to the json response
                return BadRequest(new { result.Message, Errors = dictionary });
            }

            _logger.LogInformation("User logged in: {0}", result.User.Id);

            // Return user login json
            return await GetUserLoginResult(result.User);
        }

        /// <summary>
        /// Gets the user login result.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>An IActionResult.</returns>
        private async Task<IActionResult> GetUserLoginResult(SystemUser user)
        {
            // get user privileges dto
            var dto = await _privilegesService.GetUserPrivilegesAsync(user.Id);
            
            // get associated company ids from auth service
            dto.CompanyIds = _authService.GetAssociatedCompaniesId(user.Id);

            // valid user found, generate JWT token against the fetched user
            var tokenResponse = await _tokenService.GenerateJWTToken(user, dto.CompanyIds);

            // check if user must change password
            dto.MustChangePassword = await CheckIfUserMustChangePassword(user.Id);

            // return current password policy
            dto.PasswordPolicy = new PasswordPolicy(ConfigurationHelper.Read(_context, _mapper));

            // remove user companies data
            dto.UserCompanies = null;

            // Save user session
            var result = await _sessionManager.Login(user.Id, tokenResponse.Jti);

            // Pass the user details and the generated token to the response
            if (result)
            {
                return Ok(new
                {
                    user = dto,
                    token = tokenResponse.Token
                });
            }

            // If Session cannot be started return bad request
            return BadRequest();
        }

        /// <summary>
        /// Checks if user must change password.
        /// </summary>
        /// <param name="systemUserId">The system user id.</param>
        /// <returns>True if user must change password, else returns false.</returns>
        private async Task<bool> CheckIfUserMustChangePassword(int systemUserId)
        {
            var query = _context.PasswordHistories.AsQueryable()
                                    .Where(x => x.SystemUserId == systemUserId)
                                    .OrderByDescending(x => x.Id)
                                    .Take(1);

            var history = await query.FirstOrDefaultAsync();
            if (history != null)
            {
                var diff = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - history.ChangedAt;
                var timeSpan = TimeSpan.FromSeconds(diff);

                return timeSpan.Days >= ForcePasswordChangeDays;
            }

            return false;
        }

        /// <summary>
        /// Verifies the email of newly registered user.
        /// </summary>
        /// <param name="dto">The dto containing user identity and verification code.</param>
        /// <returns>A Task of action result of response.</returns>
        [HttpGet(nameof(VerifyEmail))]
        public async Task<IActionResult> VerifyEmail([FromQuery] VerifyEmailDTO dto)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest("Request is not valid.");
            }

            _logger.LogInformation("Verify email link requested for UserId: {0}", dto.UserId);

            // Verifying user email
            var result = await _authService.VerifyEmail(dto);

            if (result.Error != null || !result.IdentityResult.Succeeded)
                return BadRequest(result.Message);

            if (dto.Flag == 0)
                _logger.LogInformation("User email token verified successfully: {0}", result.User.Email);
            else
                _logger.LogInformation("User password reset token verified successfully: {0}", result.User.Id);

            // Return user login json
            return await GetUserLoginResult(result.User);
        }

        /// <summary>
        /// Verifies the password reset token of the user.
        /// </summary>
        /// <param name="dto">The dto containing user identity and verification code.</param>
        /// <returns>A Task of action result of response.</returns>
        [HttpPost(nameof(ForgotPassword))]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO dto)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }

            _logger.LogInformation("Forgot password requested for email: {0}", dto.Email);

            string errorMsg = await _authService.SendPasswordResetLink(dto.Email);
            if (string.IsNullOrEmpty(errorMsg))
            {
                _logger.LogInformation("Password reset link sent to email: {0}", dto.Email);
                return Ok(MsgKeys.PasswordResetSent);
            }
            return BadRequest(errorMsg);
        }
    }
}