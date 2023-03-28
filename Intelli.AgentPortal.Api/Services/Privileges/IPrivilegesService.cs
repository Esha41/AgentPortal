
using Intelli.AgentPortal.Api.DTO;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Services
{
    /// <summary>
    /// The privileges service.
    /// </summary>
    public interface IPrivilegesService
    {
        /// <summary>
        /// Gets the user privileges DTO async.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>An UserReadPrivilegesDTO object.</returns>
        Task<UserReadPrivilegesDTO> GetUserPrivilegesAsync(int userId);
    }
}