using Intelli.AgentPortal.Api.Services.Cache;
using Intelli.AgentPortal.Domain.Database;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Services.Session.Impl
{
    /// <summary>
    /// The session manager based on jwt token claims.
    /// The session manager keeps track of active users in database and cache.
    /// Cache will be used to quickly access user during ping requests.
    /// Database records will be used, as backup to cache, in case IIS restarts,
    /// or across multiple installations.
    /// This will implement users per company restriction.
    /// </summary>
    public class SessionManager : ISessionManager
    {
        private readonly IGenericCache<string> _cache;
        private readonly AgentPortalContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionManager"/> class.
        /// </summary>
        /// <param name="cache">The session cache.</param>
        /// <param name="context">The AgentPortalContext.</param>
        public SessionManager(IGenericCache<string> cache, AgentPortalContext context)
        {
            _cache = cache;
            _context = context;
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="sessionId">The jti (Unique identifier for jwt token).</param>
        public async Task<bool> Login(int userId, string sessionId)
        {
            // Save session in database context
            var entity = await _context.UserSessions.FindAsync(userId);
            if (entity != null)
            {
                entity.SessionId = sessionId;
                _context.UserSessions.Update(entity);
            }
            else
            {
                entity = new Domain.Model.UserSession
                {
                    SystemUserId = userId,
                    SessionId = sessionId
                };
                await _context.UserSessions.AddAsync(entity);
            }

            // Save changes in database
            await _context.SaveChangesAsync();

            // Save session in cache for later access
            await _cache.SetValueAsync(userId, sessionId);

            // Return success
            return true;
        }

        /// <summary>
        /// Logouts the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="sessionId">The session id.</param>
        public async Task<bool> Logout(int userId, string sessionId)
        {
            // Check if session requested to logged out exists or not
            bool isActive = await IsActive(userId, sessionId);

            // If this session does not exists, user already logged out
            if (!isActive) return true;

            // Remove session from database context if exists
            var entity = await _context.UserSessions.FindAsync(userId);
            if (entity != null)
            {
                // Remove session from database context
                _context.UserSessions.Remove(entity);

                // Save changes in database
                await _context.SaveChangesAsync();
            }

            // Clear session from cache
            await _cache.SetValueAsync(userId, string.Empty);

            // Return success
            return true;
        }

        /// <summary>
        /// Is particular session active.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>A bool.</returns>
        public async Task<bool> IsActive(int userId)
        {
            // Check if cache has this users's session record
            var result = await _cache.HasValueAsync(userId);
            if (result)
            {
                // Get value from cache
                var value = await _cache.GetValueAsync(userId);

                // If it has session id then session is active
                result = !string.IsNullOrEmpty(value);

                // return result
                return result;
            }

            // Check database record if no record found in cache
            var entity = await _context.UserSessions.FindAsync(userId);
            if (entity != null)
            {
                // If record exists in database update cache too
                await _cache.SetValueAsync(userId, entity.SessionId);

                // return true if record exists
                return true;
            }
            else
            {
                // If session is not active update cache
                await _cache.SetValueAsync(userId, string.Empty);
            }

            // return false if session is not active
            return false;
        }

        /// <summary>
        /// Is session active.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="sessionId">The jti (Unique identifier for jwt token).</param>
        /// <returns>A bool.</returns>
        public async Task<bool> IsActive(int userId, string sessionId)
        {
            // Check if cache has this users's session record
            var result = await _cache.HasValueAsync(userId);
            if (result)
            {
                // Get value from cache
                var value = await _cache.GetValueAsync(userId);

                // Check if asked session id matches session id in cache
                result = value == sessionId;

                // Return result (True if session ids are same else return false)
                return result;
            }

            // Check database record if no record found in cache
            var entity = await _context.UserSessions.FindAsync(userId);
            if (entity != null)
            {
                // If record exists in database update cache too
                await _cache.SetValueAsync(userId, entity.SessionId);

                // Check if asked session id matches session id in database
                result = entity.SessionId == sessionId;

                // Return result (True if session ids are same else return false)
                return result;
            }
            else
            {
                // If session is not active update cache
                await _cache.SetValueAsync(userId, string.Empty);
            }

            // return false if session is not active
            return false;
        }
    }
}
