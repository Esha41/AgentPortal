using Intelli.AgentPortal.Api.DTO;
using Intelli.AgentPortal.Api.Services.Batches;
using Intelli.AgentPortal.Api.Services.BatchStatusService;
using Intelli.AgentPortal.Domain.Core.Helpers;
using Intelli.AgentPortal.Domain.Core.Repository;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Domain.Repository;
using Intelli.AgentPortal.Domain.Repository.Impl;
using Intelli.AgentPortal.EventBus.RabbitMQ.Event;
using Intelli.AgentPortal.EventBus.RabbitMQ.Sender;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Intelli.AgentPortal.Shared.Mvc.DocumentClassFields;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Services.BatchVerification.Impl
{
    /// <summary>
    /// The Batch Verification Service.
    /// </summary>
    public class BatchVerificationService : IBatchVerificationService
    {
        private readonly IDocumentClassFields _documentClassFieldService;
        private readonly IBatchService _batchService;
        private readonly IBatchStatusService _batchStatusService;
        private readonly ICustomRepository<VwAppPagesWorkflowStepsPerBatchSource> _repositoryVwAppPagesWorkflowStepsPerBatchSource;
        private readonly ICustomRepository<BatchMetum> _repositoryBatchMetum;
        private readonly ICustomRepository<BatchHistoryMetum> _repositoryBatchHistoryMetum;
        private readonly IRepository<Batch> _repositoryBatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchVerificationService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="documentClassFieldService">The documentClassFieldService.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="batchStatusService">The batchStatusService.</param>
        public BatchVerificationService(AgentPortalContext context,
            IDocumentClassFields documentClassFieldService,
            IBatchService batchService,
            IBatchStatusService batchStatusService,
            IEventSender sender)
        {
            _documentClassFieldService = documentClassFieldService;
            _batchStatusService = batchStatusService;
            _batchService = batchService;
            _repositoryVwAppPagesWorkflowStepsPerBatchSource = new CustomRepository<VwAppPagesWorkflowStepsPerBatchSource>(context);
            _repositoryBatchHistoryMetum = new CustomRepository<BatchHistoryMetum>(context);
            _repositoryBatchMetum = new CustomRepository<BatchMetum>(context);
            _repositoryBatch = new GenericRepository<Batch>(context);

            ((GenericRepository<Batch>)_repositoryBatch).AfterSave =
            ((CustomRepository<BatchMetum>)_repositoryBatchMetum).AfterSave =
            ((CustomRepository<BatchHistoryMetum>)_repositoryBatchHistoryMetum).AfterSave = (logs) =>
               sender.SendEvent(new MQEvent<List<AuditEntry>>(BaseController.AUDIT_EVENT_NAME, (List<AuditEntry>)logs));
        }

        /// <summary>
        /// Verify batch for agent video.
        /// </summary>
        /// <param name="verifyBatchModel">The verifyBatchModel.</param>
        /// <param name="username">The username.</param>
        /// <returns>A bool.</returns>
        public async Task<bool> VerifyBatch(VerifyBatchByAgentDTO verifyBatchModel, string username)
        {
            int nextStepsCount = 0;
            bool verified = false;
            var batchStatusCallVerifiedId = _batchStatusService.GetBatchStatusIdByEnumValue(BatchStatusesKeys.CALL_VERIFIED);

            var batch = await _batchService.GetBatchByToken(verifyBatchModel.RequestId);
            var steps = _repositoryVwAppPagesWorkflowStepsPerBatchSource.Query(d => d.BatchSourceId == batch.BatchSourceId).Select(x => x.CurrentAppPage).ToList();
            var videoStepIndex = steps.IndexOf("Video") + 1;
            nextStepsCount = steps.Count() - (videoStepIndex);

            if (batch.BatchStatusId <= batchStatusCallVerifiedId && nextStepsCount <= 1)
            {
                var batchStatusVerifiedId = _batchStatusService.GetBatchStatusIdByEnumValue(BatchStatusesKeys.VERIFIED);
                verifyBatchModel.batchStatusId = batchStatusVerifiedId;
            }
            verified = await InsertVerificationResults(verifyBatchModel, username);
            return verified;
        }

        /// <summary>
        /// Insert/update Verification Results.
        /// </summary>
        /// <param name="verifyBatchByAgent">The VerifyBatchByAgentDTO.</param>
        /// <param name="username">The username.</param>
        /// <returns>A bool.</returns>
        public async Task<bool> InsertVerificationResults(VerifyBatchByAgentDTO verifyBatchByAgent, string username)
        {
            var batch = await _batchService.GetBatchByToken(verifyBatchByAgent.RequestId);
            var lastBatchHistory = _batchService.GetBatchHistoryByBatchId(batch.Id);
            var batchMetas = _batchService.GetBatchMetaListByBatchId(batch.Id);
            await using (var trans = _repositoryBatch.GetTransaction())
            {
                try
                {
                    //update batch history meta
                    if (lastBatchHistory != null)
                    {
                        var batchHistoryMetas = _batchService.GetBatchHistoryMetaListByHistoryId(lastBatchHistory.Id);
                        UpdateFreeWillResult(batchHistoryMetas, verifyBatchByAgent.FreeWill.ToString(), lastBatchHistory.Id, username, trans, batch.RequestId);
                        UpdateIsAliveResult(batchHistoryMetas, verifyBatchByAgent.IsAlive.ToString(), lastBatchHistory.Id, username, trans, batch.RequestId);
                    }
                    //update batch meta
                    UpdateClientConcents(batchMetas, verifyBatchByAgent.ClientConsents.ToString(), batch.Id, username, trans, batch.RequestId);
                    UpdateVideoSessionResult(batchMetas, verifyBatchByAgent.VideoSessionResult.ToString(), batch.Id, username, trans, batch.RequestId);
                    UpdateVideoValidity(batchMetas, verifyBatchByAgent.VideoValidity.ToString(), batch.Id, username, trans, batch.RequestId);
                    UpdateVideoVerificationStatus(batchMetas, verifyBatchByAgent.VideoVerificationStatus.ToString(), batch.Id, username, trans, batch.RequestId);
                    UpdateSimilarityByAgent(batchMetas, verifyBatchByAgent.FaceMatching.ToString(), batch.Id, username, trans, batch.RequestId);
                    UpdateVerificationStatus(batchMetas, verifyBatchByAgent.VerificationStatus.ToString(), batch.Id, username, trans, batch.RequestId);
                    UpdateBatchStatus(batch, verifyBatchByAgent.batchStatusId, username, trans, batch.RequestId);

                    // Commit transaction
                    await trans.CommitAsync();
                }
                catch
                {
                    // Rollback transaction
                    await trans.RollbackAsync();
                    // throw exception to controller
                    throw;
                }
            }
            return true;
        }

        /// <summary>
        /// Insert/update ClientConcents.
        /// </summary>
        /// <param name="batchMetas">The batchMetas list.</param>
        /// <param name="username">The username.</param>
        /// <param name="fieldValue">The updated value.</param>
        /// <param name="batchId">The batchId.</param>
        /// <param name="trans">The ITransactionHandler.</param>
        private void UpdateClientConcents(List<BatchMetum> batchMetas, string fieldValue, int batchId, string username, ITransactionHandler trans, string requestId)
        {
            var docClassFieldClientConsentsId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.Consent).Id;
            BatchMetum clientConsentsMeta = batchMetas.Where(bm => bm.DocumentClassFieldId == docClassFieldClientConsentsId).FirstOrDefault();
            if (clientConsentsMeta != null)
            {
                clientConsentsMeta.FieldValue = fieldValue;
                var batchMetum = _repositoryBatchMetum.Query(d => d.Id == clientConsentsMeta.Id).FirstOrDefault();
                batchMetum.FieldValue = clientConsentsMeta.FieldValue;
                _repositoryBatchMetum.Update(batchMetum);
            }
            else
            {
                clientConsentsMeta = new BatchMetum()
                {
                    BatchId = batchId,
                    DocumentClassFieldId = docClassFieldClientConsentsId,
                    FieldValue = fieldValue
                };
                _repositoryBatchMetum.Insert(clientConsentsMeta);
            }
            _repositoryBatchMetum.SaveChanges(username, trans, requestId);
        }

        /// <summary>
        /// Insert/update Video Session Result.
        /// </summary>
        /// <param name="batchMetas">The batchMetas list.</param>
        /// <param name="username">The username.</param>
        /// <param name="fieldValue">The updated value.</param>
        /// <param name="batchId">The batchId.</param>
        /// <param name="trans">The ITransactionHandler.</param>
        private void UpdateVideoSessionResult(List<BatchMetum> batchMetas, string fieldValue, int batchId, string username, ITransactionHandler trans, string requestId)
        {
            var docClassFieldVideoSessionResultId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.VideoSessionResult).Id;

            BatchMetum videoSessionResultMeta = batchMetas.Where(b => b.BatchId == batchId && b.DocumentClassFieldId == docClassFieldVideoSessionResultId).FirstOrDefault();
            if (videoSessionResultMeta != null)
            {
                videoSessionResultMeta.FieldValue = string.IsNullOrEmpty(fieldValue) ? "Not-Set" : fieldValue;

                var batchMetum = _repositoryBatchMetum.Query(d => d.Id == videoSessionResultMeta.Id).FirstOrDefault();
                batchMetum.FieldValue = videoSessionResultMeta.FieldValue;
                _repositoryBatchMetum.Update(batchMetum);
            }
            else
            {
                videoSessionResultMeta = new BatchMetum()
                {
                    BatchId = batchId,
                    DocumentClassFieldId = docClassFieldVideoSessionResultId,
                    FieldValue = string.IsNullOrEmpty(fieldValue) ? "Not-Set" : fieldValue
                };
                _repositoryBatchMetum.Insert(videoSessionResultMeta);
            }
            _repositoryBatchMetum.SaveChanges(username, trans, requestId);
        }

        /// <summary>
        /// Insert/update Video Validity.
        /// </summary>
        /// <param name="batchMetas">The batchMetas list.</param>
        /// <param name="username">The username.</param>
        /// <param name="fieldValue">The updated value.</param>
        /// <param name="batchId">The batchId.</param>
        /// <param name="trans">The ITransactionHandler.</param>
        private void UpdateVideoValidity(List<BatchMetum> batchMetas, string fieldValue, int batchId, string username, ITransactionHandler trans, string requestId)
        {
            var docClassFieldVideoValidityId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.VideoValidity).Id;

            BatchMetum videoValidityMeta = batchMetas.Where(b => b.BatchId == batchId && b.DocumentClassFieldId == docClassFieldVideoValidityId).FirstOrDefault();
            if (videoValidityMeta != null)
            {
                videoValidityMeta.FieldValue = fieldValue;

                var batchMetum = _repositoryBatchMetum.Query(d => d.Id == videoValidityMeta.Id).FirstOrDefault();
                batchMetum.FieldValue = videoValidityMeta.FieldValue;
                _repositoryBatchMetum.Update(batchMetum);
            }
            else
            {
                videoValidityMeta = new BatchMetum()
                {
                    BatchId = batchId,
                    DocumentClassFieldId = docClassFieldVideoValidityId,
                    FieldValue = fieldValue
                };
                _repositoryBatchMetum.Insert(videoValidityMeta);
            }
            _repositoryBatchMetum.SaveChanges(username, trans, requestId);
        }

        /// <summary>
        /// Insert/update VideoVerificationStatus.
        /// </summary>
        /// <param name="batchMetas">The batchMetas list.</param>
        /// <param name="username">The username.</param>
        /// <param name="fieldValue">The updated value.</param>
        /// <param name="batchId">The batchId.</param>
        /// <param name="trans">The ITransactionHandler.</param>
        private void UpdateVideoVerificationStatus(List<BatchMetum> batchMetas, string fieldValue, int batchId, string username, ITransactionHandler trans, string requestId)
        {
            var docClassVideoVerificationStatusId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.VideoVerificationStatus).Id;

            BatchMetum videoVerificationStatusMeta = batchMetas.Where(b => b.BatchId == batchId && b.DocumentClassFieldId == docClassVideoVerificationStatusId).FirstOrDefault();
            if (videoVerificationStatusMeta != null)
            {
                videoVerificationStatusMeta.FieldValue = string.IsNullOrEmpty(fieldValue) ? "Not-Set" : fieldValue;

                var batchMetum = _repositoryBatchMetum.Query(d => d.Id == videoVerificationStatusMeta.Id).FirstOrDefault();
                batchMetum.FieldValue = videoVerificationStatusMeta.FieldValue;
                _repositoryBatchMetum.Update(batchMetum);
            }
            else
            {
                videoVerificationStatusMeta = new BatchMetum()
                {
                    BatchId = batchId,
                    DocumentClassFieldId = docClassVideoVerificationStatusId,
                    FieldValue = string.IsNullOrEmpty(fieldValue) ? "Not-Set" : fieldValue
                };
                _repositoryBatchMetum.Insert(videoVerificationStatusMeta);
            }
            _repositoryBatchMetum.SaveChanges(username, trans, requestId);
        }

        /// <summary>
        /// Insert/update SimilarityByAgent.
        /// </summary>
        /// <param name="batchMetas">The batchMetas list.</param>
        /// <param name="username">The username.</param>
        /// <param name="fieldValue">The updated value.</param>
        /// <param name="batchId">The batchId.</param>
        /// <param name="trans">The ITransactionHandler.</param>
        private void UpdateSimilarityByAgent(List<BatchMetum> batchMetas, string fieldValue, int batchId, string username, ITransactionHandler trans, string requestId)
        {
            var docClassFieldSimilarityId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.SimilarityByAgent).Id;

            BatchMetum similarityByAgentMeta = batchMetas.Where(b => b.BatchId == batchId && b.DocumentClassFieldId == docClassFieldSimilarityId).FirstOrDefault();
            if (similarityByAgentMeta != null)
            {
                similarityByAgentMeta.FieldValue = string.IsNullOrEmpty(fieldValue) ? "Not-Set" : fieldValue;

                var batchMetum = _repositoryBatchMetum.Query(d => d.Id == similarityByAgentMeta.Id).FirstOrDefault();
                batchMetum.FieldValue = similarityByAgentMeta.FieldValue;
                _repositoryBatchMetum.Update(batchMetum);
            }
            else
            {
                similarityByAgentMeta = new BatchMetum()
                {
                    BatchId = batchId,
                    DocumentClassFieldId = docClassFieldSimilarityId,
                    FieldValue = string.IsNullOrEmpty(fieldValue) ? "Not-Set" : fieldValue
                };
                _repositoryBatchMetum.Insert(similarityByAgentMeta);
            }
            _repositoryBatchMetum.SaveChanges(username, trans, requestId);
        }

        /// <summary>
        /// Insert/update VerificationStatus.
        /// </summary>
        /// <param name="batchMetas">The batchMetas list.</param>
        /// <param name="username">The username.</param>
        /// <param name="fieldValue">The updated value.</param>
        /// <param name="batchId">The batchId.</param>
        /// <param name="trans">The ITransactionHandler.</param>
        private void UpdateVerificationStatus(List<BatchMetum> batchMetas, string fieldValue, int batchId, string username, ITransactionHandler trans, string requestId)
        {
            var docClassFieldVerificationStatusId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.VerificationStatus).Id;

            BatchMetum verificationStatusMeta = batchMetas.Where(b => b.BatchId == batchId && b.DocumentClassFieldId == docClassFieldVerificationStatusId).FirstOrDefault();
            if (verificationStatusMeta != null)
            {
                verificationStatusMeta.FieldValue = fieldValue;

                var batchMetum = _repositoryBatchMetum.Query(d => d.Id == verificationStatusMeta.Id).FirstOrDefault();
                batchMetum.FieldValue = verificationStatusMeta.FieldValue;
                _repositoryBatchMetum.Update(batchMetum);
            }
            else
            {
                verificationStatusMeta = new BatchMetum()
                {
                    BatchId = batchId,
                    DocumentClassFieldId = docClassFieldVerificationStatusId,
                    FieldValue = fieldValue
                };
                _repositoryBatchMetum.Insert(verificationStatusMeta);
            }
            _repositoryBatchMetum.SaveChanges(username, trans, requestId);
        }

        /// <summary>
        /// Insert/update Batch Status.
        /// </summary>
        /// <param name="batchMetas">The batchMetas list.</param>
        /// <param name="username">The username.</param>
        /// <param name="fieldValue">The updated value.</param>
        /// <param name="batchId">The batchId.</param>
        /// <param name="trans">The ITransactionHandler.</param>
        private void UpdateBatchStatus(Batch batch, int newBatchStatusId, string username, ITransactionHandler trans, string requestId)
        {
            batch.BatchStatusId = newBatchStatusId;
            _repositoryBatch.Update(batch);
            _repositoryBatch.SaveChanges(username, trans, requestId);
        }

        /// <summary>
        /// Insert/update FreeWillResult.
        /// </summary>
        /// <param name="batchHistoryMetas">The batchHistoryMetas list.</param>
        /// <param name="username">The username.</param>
        /// <param name="fieldValue">The updated value.</param>
        /// <param name="batchHistoryId">The batchHistoryId.</param>
        /// <param name="trans">The ITransactionHandler.</param>
        private void UpdateFreeWillResult(List<BatchHistoryMetum> batchHistoryMetas, string fieldValue, int batchHistoryId, string username, ITransactionHandler trans, string requestId)
        {
            var docClassFieldFreeWillId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.FreeWill).Id;

            BatchHistoryMetum freeWillMeta = batchHistoryMetas.Where(bm => bm.DocumentClassFieldId == docClassFieldFreeWillId).FirstOrDefault();
            if (freeWillMeta != null)
            {
                freeWillMeta.FieldValue = fieldValue;

                var batchHistoryMetum = _repositoryBatchHistoryMetum.Query(d => d.Id == freeWillMeta.Id).FirstOrDefault();
                batchHistoryMetum.FieldValue = freeWillMeta.FieldValue;
                _repositoryBatchHistoryMetum.Update(batchHistoryMetum);
            }
            else
            {
                freeWillMeta = new BatchHistoryMetum()
                {
                    BatchHistoryId = batchHistoryId,
                    DocumentClassFieldId = docClassFieldFreeWillId,
                    FieldValue = fieldValue
                };
                _repositoryBatchHistoryMetum.Insert(freeWillMeta);
            }
            _repositoryBatchHistoryMetum.SaveChanges(username, trans, requestId);
        }

        /// <summary>
        /// Insert/update IsAliveResult.
        /// </summary>
        /// <param name="batchHistoryMetas">The batchHistoryMetas list.</param>
        /// <param name="username">The username.</param>
        /// <param name="fieldValue">The updated value.</param>
        /// <param name="batchHistoryId">The batchHistoryId.</param>
        /// <param name="trans">The ITransactionHandler.</param>
        private void UpdateIsAliveResult(List<BatchHistoryMetum> batchHistoryMetas, string fieldValue, int batchHistoryId, string username, ITransactionHandler trans, string requestId)
        {
            var docClassFieldIsAlive = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.IsAlive).Id;

            BatchHistoryMetum isAlive = batchHistoryMetas.Where(bm => bm.DocumentClassFieldId == docClassFieldIsAlive).FirstOrDefault();
            if (isAlive != null)
            {
                isAlive.FieldValue = fieldValue;
                var batchHistoryMetum = _repositoryBatchHistoryMetum.Query(d => d.Id == isAlive.Id).FirstOrDefault();
                batchHistoryMetum.FieldValue = isAlive.FieldValue;
                _repositoryBatchHistoryMetum.Update(batchHistoryMetum);
            }
            else
            {
                isAlive = new BatchHistoryMetum()
                {
                    BatchHistoryId = batchHistoryId,
                    DocumentClassFieldId = docClassFieldIsAlive,
                    FieldValue = fieldValue
                };
                _repositoryBatchHistoryMetum.Insert(isAlive);
            }
            _repositoryBatchHistoryMetum.SaveChanges(username, trans, requestId);
        }

    }
}
