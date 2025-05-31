using Intelli.AgentPortal.Api.Services.Session;
using Intelli.AgentPortal.Shared.Mvc;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Controllers.v1
{
    /// <summary>
    /// The logout controller.
    /// Following controller is responsible for logging out user from session manager.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LogoutController : BaseController
    {
        private readonly ISessionManager _sessionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogoutController"/> class.
        /// </summary>
        /// <param name="sessionManager">The session manager.</param>
        public LogoutController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        /// <summary>
        /// Logout user.
        /// </summary>
        /// <returns>A Task.</returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            int userId = HttpContext.GetUserId();
            string jti = HttpContext.GetJti();

            return await Logout(userId, jti);
        }

        /// <summary>
        /// Logout user.
        /// </summary>
        /// <returns>A Task.</returns>
        private async Task<IActionResult> Logout(int userId, string jti)
        {
            var result = await _sessionManager.Logout(userId, jti);

            return result ? Ok("success") : BadRequest("failure");
        }
    }
}