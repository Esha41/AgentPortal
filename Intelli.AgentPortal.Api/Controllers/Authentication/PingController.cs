using Intelli.AgentPortal.Api.Services.Session;
using Intelli.AgentPortal.Shared.Mvc;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Controllers.v1
{
    /// <summary>
    /// The ping controller.
    /// The ping call, in following controller, is used to check if user's session is already active or not.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PingController : BaseController
    {
        private readonly ISessionManager _sessionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PingController"/> class.
        /// </summary>
        /// <param name="sessionManager">The session manager.</param>
        public PingController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        /// <summary>
        /// Pings the session manager and checks if current session is alive.
        /// </summary>
        /// <returns>A Task.</returns>
        [HttpGet]
        public async Task<IActionResult> Ping()
        {
            int userId = HttpContext.GetUserId();
            string jti = HttpContext.GetJti();

            var result = await _sessionManager.IsActive(userId, jti);

            return result ? Ok("active") : BadRequest("expired");
        }
    }
}