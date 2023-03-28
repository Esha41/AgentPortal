using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Services.Session
{
    /// <summary>
    /// The session manager interface.
    /// </summary>
    public interface ISessionManager
    {
        /// <summary>
        /// Is session active.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>A bool.</returns>
        Task<bool> IsActive(int userId);

        /// <summary>
        /// Is particular session active.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="sessionId">The session id.</param>
        /// <returns>A bool.</returns>
        Task<bool> IsActive(int userId, string sessionId);

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="sessionId">The session id.</param>
        Task<bool> Login(int userId, string sessionId);

        /// <summary>
        /// Logouts the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="sessionId">The session id.</param>
        Task<bool> Logout(int userId, string sessionId);
    }
}