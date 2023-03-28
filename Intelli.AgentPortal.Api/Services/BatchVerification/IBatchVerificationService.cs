using Intelli.AgentPortal.Api.DTO;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Services.BatchVerification
{
    /// <summary>
    /// Interface for Batch Verification Service
    /// </summary>
    public interface IBatchVerificationService
    {
        /// <summary>
        /// Verify batch for agent video.
        /// </summary>
        /// <param name="verifyBatchModel">The verifyBatchModel.</param>
        /// <param name="username">The username.</param>
        /// <returns>A bool.</returns>
        Task<bool> VerifyBatch(VerifyBatchByAgentDTO verifyBatchModel, string username);
    }
}
