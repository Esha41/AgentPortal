using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;

namespace Intelli.AgentPortal.Shared.Services.Abstract
{
    /// <summary>
    /// Interface for Company Actions
    /// </summary>
    public interface ICompanyActions
    {
        /// <summary>
        /// Get company specific customer data.
        /// </summary>
        /// <param name="batch">The batch model.</param>
        /// <returns>CustomerData list.</returns>
        List<CustomerData> AgentCustomerData(Batch batch);

        /// <summary>
        /// Get company specific Prado Checks.
        /// </summary>
        /// <param name="DocumentClassId">TheDocumentClassId.</param>
        /// <returns>PradoCheck list.</returns>
        List<PradoCheck> PradoChecks(int? DocumentClassId);

        /// <summary>
        /// Get company specific DocumentsDownloadAccess.
        /// </summary>
        /// <param name="token">The batch token.</param>
        /// <returns>Dictionary.</returns>
        Dictionary<string, string> GetDocumentsDownloadAccess(string token);
    }
}
