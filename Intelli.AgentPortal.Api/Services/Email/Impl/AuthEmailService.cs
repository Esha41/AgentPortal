using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Services
{
    /// <summary>
    /// The auth service implementation
    /// </summary>
    public class AuthEmailService : IAuthEmailService
    {
        /// <summary>
        /// The email confirmation call back url.
        /// </summary>
        const string EmailConfirmationCallBackUrl = "EmailConfirmationCallBackUrl";

        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AuthService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="config">The config object to be Injected via IOC</param>
        /// <param name="emailSender">The email sender.</param>
        /// <param name="logger">Logs information and errors.</param>
        public AuthEmailService(IConfiguration config,
            IEmailSender emailSender,
            ILogger<AuthService> logger)
        {
            _config = config;
            _emailSender = emailSender;
            _logger = logger;
        }

        /// <summary>
        /// Confirms the email.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="token">Email confirmation token.</param>
        /// <param name="email">The user email address.</param>
        /// <returns>A Task.</returns>
        public async Task SendEmailConfirmation(string userId, string token, string email)
        {
            try
            {
                await _emailSender.SendEmailAsync(email,
                   "Confirm your account",
                   "Please confirm your account by clicking on the following link: <a href=\""
                                                   + GetCallbackUrl(0, userId, token, email)
                                                   + "\">Confirm Email</a>");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to send email to {1} UserId: {0}", userId, email);
                throw;
            }
        }

        /// <summary>
        /// Confirms the email.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="token">Email confirmation token.</param>
        /// <param name="email">The user email address.</param>
        /// <returns>A Task.</returns>
        public async Task SendPasswordResetLink(string userId, string token, string email)
        {
            try
            {
                await _emailSender.SendEmailAsync(email,
                   "Reset your password",
                   "Please reset your password by clicking on the following link: <a href=\""
                                                   + GetCallbackUrl(1, userId, token, email)
                                                   + "\">Reset Password</a>");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to send email to {1} UserId: {0}", userId, email);
            }
        }

        /// <summary>
        /// Gets the callback url.
        /// </summary>
        /// <param name="flag">The flag, if 0 then email token, else password reset code.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="token">The token.</param>
        /// <param name="code">The code.</param>
        /// <returns>A string.</returns>
        private string GetCallbackUrl(int flag, string userId, string token, string code)
        {
            token = Convert.ToBase64String(Encoding.Default.GetBytes(token));
            code = Convert.ToBase64String(Encoding.Default.GetBytes(code));

            return $"{_config[EmailConfirmationCallBackUrl]}?flag={flag}&userId={userId}&token={token}&code={code}";
        }
    }
}
