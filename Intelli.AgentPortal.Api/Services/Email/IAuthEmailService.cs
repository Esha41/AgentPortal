using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Services
{
    /// <summary>
    /// The auth email service interface.
    /// </summary>
    public interface IAuthEmailService
    {
        /// <summary>
        /// Confirms the email.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="token">Email confirmation token.</param>
        /// <param name="email">The user email address.</param>
        /// <returns>A Task.</returns>
        Task SendEmailConfirmation(string userId, string token, string email);

        /// <summary>
        /// Confirms the email.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="token">Email confirmation token.</param>
        /// <param name="email">The user email address.</param>
        /// <returns>A Task.</returns>
        Task SendPasswordResetLink(string userId, string token, string email);
    }
}