using Intelli.AgentPortal.Api.Services.JWT.Impl;
using Intelli.AgentPortal.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Services
{
    /// <summary>
    /// The json web token service interface.
    /// </summary>
    public interface IJWTService
    {
        /// <summary>
        /// Generates the JWT Token.
        /// </summary>
        /// <param name="userInfo">The user info.</param>
        /// <returns>A string.</returns>
        Task<TokenResponseDto> GenerateJWTToken(SystemUser userInfo , List<int> CompanyIds);
    }
}