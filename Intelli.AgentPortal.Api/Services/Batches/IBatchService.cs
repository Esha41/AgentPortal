using Intelli.AgentPortal.Api.DTO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;

namespace Intelli.AgentPortal.Api.Services.Batches
{
    /// <summary>
    /// Interface for batch service
    /// </summary>
    public interface IBatchService
    {
        /// <summary>
        /// Get Batch by requestId.
        /// </summary>
        /// <param name="requestId">The batch requestId.</param>
        /// <returns>Batch.</returns>
        Task<Batch> GetBatch(string requestId);

        /// <summary>
        /// Get GetAspnetUser by system user id.
        /// </summary>
        /// <param name="userId">The userId.</param>
        /// <returns>Batch.</returns>
        int GetAspnetUser(int userId);

        /// <summary>
        /// Get Batch by token.
        /// </summary>
        /// <param name="token">The batch token.</param>
        /// <returns>Batch.</returns>
        Task<Batch> GetBatchByToken(string token);

        /// <summary>
        /// Load batch for storing image.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>Batch.</returns>
        Batch LoadBatchForSaveImage(string requestId);

        /// <summary>
        /// Load batch for BatchItem Documents.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>Batch.</returns>
        Batch LoadBatchForBatchItemDocuments(string requestId);

        /// <summary>
        /// Load batch and its all references by requestId.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <param name="userId">The current user's Id.</param>
        /// <returns>Batch.</returns>
        Batch LoadFullBatch(string requestId, int userId, bool isIndexOnlyView = false);

        /// <summary>
        /// Get meta data of batchId and documentClassFieldId.
        /// </summary>
        /// <param name="batchId">The batchId.</param>
        /// <param name="documentClassFieldId">The documentClassFieldId.</param>
        /// <returns>Batch meta field value.</returns>
        string GetBatchMeta(int batchId, int documentClassFieldId);

        /// <summary>
        /// Get BatchMeta List By Batch Id.
        /// </summary>
        /// <param name="batchId">The batchId.</param>
        /// <returns>Batch meta list .</returns>
        List<BatchMetum> GetBatchMetaListByBatchId(int batchId);

        /// <summary>
        /// get batch history of specific batchId.
        /// </summary>
        /// <param name="batchId">The batchId.</param>
        /// <returns>Batch history.</returns>
        BatchHistory GetBatchHistoryByBatchId(int batchId);

        /// <summary>
        /// Get Batch History Meta List By History Id.
        /// </summary>
        /// <param name="batchHistoryId">The batchHistoryId.</param>
        /// <returns>BatchHistoryMetum list .</returns>
        List<BatchHistoryMetum> GetBatchHistoryMetaListByHistoryId(int batchHistoryId);

        /// <summary>
        /// get batch history of specific pageId.
        /// </summary>
        /// <param name="pageId">The pageId.</param>
        /// <returns>Batch history.</returns>
        BatchHistory GetBatchHistoryByPageId(int pageId);

        /// <summary>
        /// get batch history list of specific batchId.
        /// </summary>
        /// <param name="batchId">The batchId.</param>
        /// <returns>Batch history list.</returns>
        List<BatchHistory> GetBatchHistories(int batchId);

        /// <summary>
        /// get BatchHistoryItemPage of specific pageId.
        /// </summary>
        /// <param name="pageId">The pageId.</param>
        /// <returns>BatchHistoryItemPage list.</returns>
        List<BatchHistoryItemPage> GetBatchHistoryItemPage(int pageId);

        /// <summary>
        /// get BatchSourceUploadDoc list of specific batchSourceId.
        /// </summary>
        /// <param name="batchSourceId">The batchSourceId.</param>
        /// <returns>BatchSourceUploadDoc list.</returns>
        List<BatchSourceUploadDocDTO> BatchSourceUploadDoc(int batchSourceId);

        /// <summary>
        /// get all pending batches.
        /// </summary>
        /// <param name="agentId">The agentId.</param>
        /// <returns>PendingBatchesDTO list.</returns>
        Task<PendingBatchesDTO> GetPendingBatchPage(int agentId);

        /// <summary>
        /// List of CustomerData by using factory pattern.
        /// </summary>
        /// <param name="batch">The batch model.</param>
        /// <returns>List of CustomerData.</returns>
        List<CustomerData> GetCustomerData(Batch batch);

        /// <summary>
        ///get files list in Base64.
        /// </summary>
        /// <param name="batch">The batch model.</param>
        /// <returns>List of UploadedDocumentsDTO.</returns>
        List<UploadedDocumentsDTO> GetAllSignedDocuments(Batch batch);

        /// <summary>
        ///VwDocumentGroupNamesFormBatchHistory list.
        /// </summary>
        /// <param name="ids">batch history Id list.</param>
        /// <returns>view .</returns>
        List<VwDocumentGroupNamesFormBatchHistory> GetBatchHistorItems(List<int> ids);

        /// <summary>
        ///VwGetBatchHistoryItemField list.
        /// </summary>
        /// <param name="batchHistoryItemId">batchHistoryItemId.</param>
        /// <returns>view .</returns>
        Task<List<VwGetBatchHistoryItemField>> GetBatchHistoryItemFields(int batchHistoryItemId);

        /// <summary>
        ///VwGetBatchItemField list.
        /// </summary>
        /// <param name="batchItemId">batchHistoryItemId.</param>
        /// <returns>view .</returns>
        Task<List<VwGetBatchItemField>> GetBatchItemFields(int batchItemId);

        /// <summary>
        /// Get Company By Id.
        /// </summary>
        /// <param name="id">The companyId.</param>
        /// <returns>Company.</returns>
        Company GetCompanyById(int id);

        /// <summary>
        /// Get Company By name.
        /// </summary>
        /// <param name="name">The company name.</param>
        /// <returns>Company.</returns>
        Company GetCompanyByName(string name);

        /// <summary>
        /// get agentviewModel for agent view screen.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="userName">The userName.</param>
        /// <param name="userId">TheuserId.</param>
        /// <returns>AgentViewModel.</returns>
        AgentViewDTO GetAgentIndex(string requestId, string userName, int userId);

        /// <summary>
        /// get agentviewModel for agent video screen.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="userName">The userName.</param>
        /// <param name="userId">TheuserId.</param>
        /// <returns>AgentViewModel.</returns>
        AgentViewDTO GetAgentVideoIndex(string requestId, string userName, int userId);

        /// <summary>
        /// get agentviewModel for agent video With Jumio screen.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="userName">The userName.</param>
        /// <param name="userId">TheuserId.</param>
        /// <returns>AgentViewModel.</returns>
        AgentViewDTO GetAgentVideoWithJumio(string requestId, string userName, int userId);

        /// <summary>
        /// get VerificationResult for edit operation.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="batchHistoryItemId">The batchHistoryItemId.</param>
        /// <returns>GetVerificationResultDTO.</returns>
        Task<GetVerificationResultDTO> GetEditVerificationResult(string requestId, int batchHistoryItemId);

        /// <summary>
        /// get txn VerificationResult for edit operation.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="batchItemId">The batchItemId.</param>
        /// <returns>GetVerificationTxnDataDTO.</returns>
        Task<GetVerificationTxnDataDTO> GetVerificationTxnData(string requestId, int batchItemId);

        /// <summary>
        /// Get Batch History Item By Id.
        /// </summary>
        /// <param name="batchHistoryItemId">The batchHistoryItemId.</param>
        /// <returns>BatchHistoryItem.</returns>
        BatchHistoryItem GetBatchHistoryItemById(int batchHistoryItemId);

        /// <summary>
        /// update batch lockedBy field by agent Id.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="userId">The agent Id.</param>
        /// <param name="UserName">The agent name.</param>
        /// <returns>bool result.</returns>
        Task<bool> UpdateBatchLocked(string requestId, int userId, string UserName);

        /// <summary>
        /// unlocked specific batch.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="userId">The agent Id.</param>
        /// <param name="UserName">The agent name.</param>
        /// <returns>bool result.</returns>
        Task<bool> UpdateBatchUnlocked(string requestId, int userId, string UserName);

        /// <summary>
        /// Verify Batch For Agent Jumio.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="username">The agent name.</param>
        /// <returns>bool result.</returns>
        Task<bool> VerifyBatchForAgentJumio(string requestId, string username);

        /// <summary>
        /// Update specific Batch Status.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="UserName">The agent name.</param>
        /// <param name="batchStatus">The batchStatus.</param>
        /// <returns>bool result.</returns>
        Task<bool> UpdateBatchStatus(string requestId, string batchStatus, string UserName);

        /// <summary>
        /// Update Batch Meta With Batch Id.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="UserName">The agent name.</param>
        /// <param name="batchMetaValue">The batchMetaValue.</param>
        /// <returns>bool result.</returns>
        bool UpdateBatchMetaWithBatchId(string batchMetaValue, int batchMetaDocumentClassFieldId, int batchHistoryId, string UserName, string requestId);

        /// <summary>
        /// Update Batch history Meta With Batch history Id.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="UserName">The agent name.</param>
        /// <param name="historyMetaId">The historyMetaId.</param>
        /// <returns>bool result.</returns>
        Task<bool> UpdateBatchHistoryMetaByHistoryId(string historyMetaValue, int historyMetaId, string UserName, string requestId);

        /// <summary>
        /// Edit verification results.
        /// </summary>
        /// <param name="updatedFields">The EditVerificationResultDTO.</param>
        /// <param name="username">The agent name.</param>
        /// <returns>bool result.</returns>
        bool UpdateBatchHistoryItemField(List<EditVerificationResultDTO> updatedFields, string username);

        /// <summary>
        /// Edit verification txn results.
        /// </summary>
        /// <param name="updatedFields">The EditTxnVerificationResultDTO.</param>
        /// <param name="username">The agent name.</param>
        /// <returns>bool result.</returns>
        bool UpdateBatchItemFields(List<EditTxnVerificationResultDTO> updatedFields, string username);

        /// <summary>
        /// Save Captured Image by agent.
        /// </summary>
        /// <param name="capturedImageDTO">The CapturedImageDTO.</param>
        /// <param name="username">The agent name.</param>
        /// <returns>bool result.</returns>
        bool SaveCapturedImage(CapturedImageDTO capturedImageDTO, string username);

        /// <summary>
        /// Update Batch status For AgentIndex view.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <param name="username">The agent name.</param>
        /// <returns>bool result.</returns>
        Task<bool> UpdateBatchForAgentIndex(string requestId, string username);

        /// <summary>
        /// GetAgentVideoPage stored procedure for document result.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>bool result.</returns>
        AgentVideoPageDTO GetAgentVideoPage(string requestId);

        /// <summary>
        /// Get Document Base 64String by PageId.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="pageId">The pageId.</param>
        /// <returns>string result.</returns>
        Task<string> GetDocumentBase64StringbyPageId(string token, string pageId);

        /// <summary>
        /// Get All Pending Batches For Call.
        /// </summary>
        /// <returns>string result.</returns>
        List<BatchVideoPriority> GetAllPendingBatchesForCall();

        /// <summary>
        /// Get Uploaded Documents For Agent Index.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>AgentViewDTO result.</returns>
        Task<AgentViewDTO> GetUploadedDocumentsForAgentIndex(string requestId);

        /// <summary>
        /// Get Verification Results For Agent Index.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>AgentViewDTO result.</returns>
        Task<AgentViewDTO> GetVerificationResultsForAgentIndex(string requestId);

        /// <summary>
        /// Get Signed Documents For Agent Index.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>AgentViewDTO result.</returns>
        Task<AgentViewDTO> GetSignedDocumentsForAgentIndex(string requestId);

        /// <summary>
        /// Get Uploaded Documents For Agent Jumio.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>AgentViewDTO result.</returns>
        Task<AgentViewDTO> GetUploadedDocumentsForAgentJumio(string requestId);

        /// <summary>
        /// Get Verification Results For Agent Jumio.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>AgentViewDTO result.</returns>
        Task<AgentViewDTO> GetVerificationResultsForAgentJumio(string requestId);

        /// <summary>
        /// get pending batch by SRID.
        /// </summary>
        /// <param name="SRID">The SRID.</param>
        /// <returns>BatchVideoPriority.</returns>
        BatchVideoPriority GetPendingBatchBySRID(string SRID);

        /// <summary>
        /// get the list of document access(company specific).
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>KeyValuePair list.</returns>
        Task<List<KeyValuePair<string, string>>> RequestDocumentsAccess(string requestId);
    }
}
