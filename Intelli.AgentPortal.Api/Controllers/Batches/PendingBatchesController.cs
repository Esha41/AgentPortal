using AutoMapper;
using Intelli.AgentPortal.Api.DTO;
using Intelli.AgentPortal.Api.Helpers;
using Intelli.AgentPortal.Api.Services.Batches;
using Intelli.AgentPortal.Api.Services.BatchStatusService;
using Intelli.AgentPortal.Api.Services.BatchVerification;
using Intelli.AgentPortal.Api.Services.BopConfigService;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Intelli.AgentPortal.Shared.Mvc.Extensions;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Controllers.Batches
{
    /// <summary>
    /// The roles controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PendingBatchesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IBatchService _batchSerivce;
        private readonly IBatchStatusService _batchStatusService;
        private readonly IBopConfigService _bopConfigService;
        private readonly IBatchVerificationService _batchVerificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PendingBatchesController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="sender">The Event Sender.</param>
        public PendingBatchesController(IMapper mapper,
            ILogger<PendingBatchesController> logger,
             IBatchService batchSerivce,
               IBatchStatusService batchStatusService,
                IBatchVerificationService batchVerificationService,
                  IBopConfigService bopConfigService)
        {
            _logger = logger;
            _mapper = mapper;
            _batchSerivce = batchSerivce;
            _batchStatusService = batchStatusService;
            _bopConfigService = bopConfigService;
            _batchVerificationService = batchVerificationService;
        }

        /// <summary>
        /// Get all Pending Batches.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("GetAllPendingBatches")]
        public async Task<IActionResult> GetAllPendingBatches()
        {
            try
            {
                var pendingBatches = await _batchSerivce.GetPendingBatchPage(UserId);

                return Ok(new
                {
                    Items = pendingBatches
                });
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError(e, "{0}", e.Message);

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Get all Pending Batches for signalr.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("GetAllPendingBatchesForCall")]
        [AllowAnonymous]
        public IActionResult GetAllPendingBatchesForCall()
        {
            try
            {
                var pendingBatchesForVideoCall = _batchSerivce.GetAllPendingBatchesForCall();
                return Ok(new
                {
                    Items = pendingBatchesForVideoCall
                });
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError(e, "{0}", e.Message);

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Get Next Available Batch.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("GetNextBatch")]
        public async Task<IActionResult> GetNextBatch()
        {
            try
            {
                AgentVideoDTO agentVideoModel = new AgentVideoDTO();
                var aspnetUserId = _batchSerivce.GetAspnetUser(UserId);
                //get all pending batches
                var pendingBatches = await _batchSerivce.GetPendingBatchPage(UserId);
                if (pendingBatches.VideoPriority.Count() == 0)
                {
                    agentVideoModel.PendingBatchesView = true;
                    agentVideoModel.AlertMessage = MsgKeys.NoBatchInQueue;
                    return Ok(agentVideoModel);
                }

                //if video priority page is locked by login agent then procees accordingly
                //else update lockedby by current login agent
                if (pendingBatches.VideoPriority.Where(x => x.LockedBy == aspnetUserId).Count() > 0)
                {
                    var lockedBatch = pendingBatches.VideoPriority.Where(x => x.LockedBy == aspnetUserId).FirstOrDefault();
                    Company company = _batchSerivce.GetCompanyById(Convert.ToInt32(lockedBatch.CompanyId));

                    agentVideoModel.requestId = lockedBatch.RequestId;
                    agentVideoModel.AgentController = company.AgentController;
                    return Ok(agentVideoModel);
                }

                PendingBatchesVideoPriorityDTO batchVideoPriority = new PendingBatchesVideoPriorityDTO();

                var priorityBatch = pendingBatches.VideoPriority
                                        .OrderBy(x => x.Priotity)
                                        .OrderBy(x => x.CreatedDate);

                batchVideoPriority = priorityBatch.Where(x => x.LockedBy == null).FirstOrDefault();

                if (batchVideoPriority != null)
                {
                    //update lockedBy field by agentId
                    var lockedBatch = await _batchSerivce.UpdateBatchLocked(batchVideoPriority.RequestId, UserId, UserName);

                    //if updation failed then redirect to pending batches screen, else show video screen
                    if (!lockedBatch)
                    {
                        agentVideoModel.PendingBatchesView = true;
                        agentVideoModel.AlertMessage = MsgKeys.BatchLocked;
                        return Ok(agentVideoModel);
                    }

                    Company company = _batchSerivce.GetCompanyById(Convert.ToInt32(batchVideoPriority.CompanyId));

                    agentVideoModel.requestId = batchVideoPriority.RequestId;
                    agentVideoModel.AgentController = company.AgentController;
                    return Ok(agentVideoModel);
                }
                else
                {
                    agentVideoModel.PendingBatchesView = true;
                    agentVideoModel.AlertMessage = MsgKeys.AllBatchLocked;
                    return Ok(agentVideoModel);
                }
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError(e, "{0}", e.Message);

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// get pending batch by SRID.
        /// </summary>
        /// <param name="SRID">The SRID.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("GetPendingBatchBySRID/{SRID}")]
        public async Task<IActionResult> GetPendingBatchBySRID(string SRID)
        {
            try
            {
                //get pending batch
                BatchVideoPriority pendingBatch = _batchSerivce.GetPendingBatchBySRID(SRID);
                //get asp Net User Id of current agent
                int currentAgentId = _batchSerivce.GetAspnetUser(UserId);

                AgentVideoDTO agentVideoModel = new AgentVideoDTO();

                if (pendingBatch == null)
                {
                    // if batch does not exist in pending batches, return to pending batches with relevant message
                    agentVideoModel.PendingBatchesView = true;
                    agentVideoModel.AlertMessage = MsgKeys.ObjectNotExists;
                    return Ok(agentVideoModel);
                }
                else if (pendingBatch.LockedBy == null)
                {
                    // if batch is not locked, update lockedby by current logged-in agent
                    var Locked = await _batchSerivce.UpdateBatchLocked(pendingBatch.RequestId, UserId, UserName);

                    //if updation failed then redirect to pending batches screen, else show relevant agent screen
                    if (!Locked)
                    {
                        agentVideoModel.PendingBatchesView = true;
                        agentVideoModel.AlertMessage = MsgKeys.BatchLocked;
                        return Ok(agentVideoModel);
                    }
                }
                else
                {
                    if (!pendingBatch.LockedBy.Equals(currentAgentId))
                    {
                        // if batch is already locked by another agent, return to pending batches with relevant message
                        agentVideoModel.PendingBatchesView = true;
                        agentVideoModel.AlertMessage = MsgKeys.BatchLocked;
                        return Ok(agentVideoModel);
                    }
                }

                // if or when batch is locked by logged-in agent then return relevant agent screen
                Company company = _batchSerivce.GetCompanyById(Convert.ToInt32(pendingBatch.CompanyId));
                agentVideoModel.requestId = pendingBatch.RequestId;
                agentVideoModel.AgentController = company.AgentController;
                return Ok(agentVideoModel);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<PendingBatchesController>(e.Message, e, SRID, nameof(GetPendingBatchBySRID));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// get agent video view result.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("GetAgentVideoIndex/{requestId}")]
        public IActionResult GetAgentVideoIndex(string requestId)
        {
            try
            {
                AgentViewDTO agentViewModel = _batchSerivce.GetAgentVideoIndex(requestId, UserName, UserId);
                if (agentViewModel == null)
                {
                    //if agentViewModel not available, then redirect to pending batches screen
                    AgentVideoDTO agentVideoModel = new AgentVideoDTO();
                    agentVideoModel.PendingBatchesView = true;
                    return Ok(agentVideoModel, MsgKeys.NotAllowed);
                }
                return Ok(agentViewModel);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<PendingBatchesController>(e.Message, e, requestId, nameof(GetAgentVideoIndex));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// get agent view result.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("GetAgentIndex/{requestId}")]
        public IActionResult GetAgentIndex(string requestId)
        {
            try
            {
                AgentViewDTO agentViewModel = _batchSerivce.GetAgentIndex(requestId, UserName, UserId);
                if (agentViewModel == null)
                {
                    //if agentViewModel not available, then redirect to pending batches screen
                    AgentVideoDTO agentVideoModel = new AgentVideoDTO();
                    agentVideoModel.PendingBatchesView = true;
                    return Ok(agentVideoModel, MsgKeys.NotAllowed);
                }
                return Ok(agentViewModel);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<PendingBatchesController>(e.Message, e, requestId, nameof(GetAgentIndex));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// get agent video with jumio view result.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("GetAgentVideoWithJumio/{requestId}")]
        public IActionResult GetAgentVideoWithJumio(string requestId)
        {
            try
            {
                AgentViewDTO agentViewModel = _batchSerivce.GetAgentVideoWithJumio(requestId, UserName, UserId);
                if (agentViewModel == null)
                {
                    //if agentViewModel not available, then redirect to pending batches screen
                    AgentVideoDTO agentVideoModel = new AgentVideoDTO();
                    agentVideoModel.PendingBatchesView = true;
                    return Ok(agentVideoModel, MsgKeys.NotAllowed);
                }
                return Ok(agentViewModel);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<PendingBatchesController>(e.Message, e, requestId, nameof(GetAgentVideoWithJumio));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Verify batch for agent video view.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpPost]
        [Route("VerifyBatch")]
        public async Task<IActionResult> VerifyBatch(VerifyBatchByAgentDTO verifyBatchByAgent)
        {
            try
            {
                var verifyBatch = await _batchVerificationService.VerifyBatch(verifyBatchByAgent, UserName);
                if (!verifyBatch)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                return Ok(verifyBatch);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<PendingBatchesController>(e.Message, e, verifyBatchByAgent.RequestId, nameof(VerifyBatch));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Verify batch for agent jumio view.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("VerifyBatchForAgentJumio/{requestId}")]
        public async Task<IActionResult> VerifyBatchForAgentJumio(string requestId)
        {
            try
            {
                var verifyBatch = await _batchSerivce.VerifyBatchForAgentJumio(requestId, UserName);

                if (!verifyBatch)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                return Ok(verifyBatch);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<PendingBatchesController>(e.Message, e, requestId, nameof(VerifyBatchForAgentJumio));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// update batch meta for prado verification.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("UpdateBatchMetaWithBatchId/{requestId}/{batchMetaValue}/{batchMetaDocumentClassFieldId}/{batchHistoryId}")]
        public async Task<IActionResult> UpdateBatchMetaWithBatchId(string requestId, string batchMetaValue, int batchMetaDocumentClassFieldId, int batchHistoryId)
        {
            try
            {
                var batch = await _batchSerivce.GetBatchByToken(requestId);

                var verifiedBatchStatusId = _batchStatusService.GetBatchStatusIdByEnumValue(BatchStatusesKeys.VERIFIED);

                if (batch.BatchStatusId >= verifiedBatchStatusId)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                var updateBatchMeta = _batchSerivce.UpdateBatchMetaWithBatchId(batchMetaValue, batchMetaDocumentClassFieldId, batchHistoryId, UserName, requestId);
                if (!updateBatchMeta)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                return Ok(updateBatchMeta);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<PendingBatchesController>(e.Message, e, requestId, nameof(UpdateBatchMetaWithBatchId));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        ///update batchHistoryMeta for jumio validation.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("UpdateBatchHistoryMetaByHistoryId/{requestId}/{historyMetaValue}/{historyMetaId}")]
        public async Task<IActionResult> UpdateBatchHistoryMetaByHistoryId(string requestId, string historyMetaValue, int historyMetaId)
        {
            try
            {
                var batch = await _batchSerivce.GetBatchByToken(requestId);

                var verifiedBatchStatusId = _batchStatusService.GetBatchStatusIdByEnumValue(BatchStatusesKeys.VERIFIED);

                if (batch.BatchStatusId >= verifiedBatchStatusId)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                var updateBatchHistoryMeta = await _batchSerivce.UpdateBatchHistoryMetaByHistoryId(historyMetaValue, historyMetaId, UserName, requestId);
                if (!updateBatchHistoryMeta)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                return Ok(updateBatchHistoryMeta);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<PendingBatchesController>(e.Message, e, requestId, nameof(UpdateBatchMetaWithBatchId));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// save captured image by agent.
        /// </summary>
        /// <param name="capturedImageDTO">The capturedImageDTO.</param>
        /// <returns>An ActionResult.</returns>
        [HttpPost]
        [Route("SaveCapturedImage")]
        public IActionResult SaveCapturedImage(CapturedImageDTO capturedImageDTO)
        {
            try
            {
                // Checking if the passed DTO is valid
                if (!ModelState.IsValid || capturedImageDTO == null)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                var saveCpturedImage = _batchSerivce.SaveCapturedImage(capturedImageDTO, UserName);
                if (!saveCpturedImage)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                return Ok(saveCpturedImage);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<PendingBatchesController>(e.Message, e, capturedImageDTO.RequestId, nameof(SaveCapturedImage));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// unlocked the specific batch.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("UpdateBatchUnlocked/{requestId}")]
        public async Task<IActionResult> UpdateBatchUnlocked(string requestId)
        {
            try
            {
                var updateBatch = await _batchSerivce.UpdateBatchUnlocked(requestId, UserId, UserName);
                if (!updateBatch)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                return Ok(updateBatch);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<PendingBatchesController>(e.Message, e, requestId, nameof(UpdateBatchUnlocked));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// locked the specific batch.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("UpdateBatchLocked/{requestId}")]
        public async Task<IActionResult> UpdateBatchLocked(string requestId)
        {
            try
            {
                var updateBatch = await _batchSerivce.UpdateBatchLocked(requestId, UserId, UserName);
                if (!updateBatch)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                return Ok(updateBatch);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<PendingBatchesController>(e.Message, e, requestId, nameof(UpdateBatchLocked));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Update Batch Status.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("UpdateBatchStatus/{requestId}")]
        public async Task<IActionResult> UpdateBatchStatus(string requestId)
        {
            try
            {
                bool updateBatch = false;
                var batch = await _batchSerivce.GetBatchByToken(requestId);
                var govGrCompanyId = _batchSerivce.GetCompanyByName(CompanyKeys.GovGr).Id;

                if (batch.CompanyId == govGrCompanyId)
                    updateBatch = await _batchSerivce.UpdateBatchStatus(requestId, BatchStatusesKeys.BUSINESS_RULES_RUN, UserName);

                else
                    updateBatch = await _batchSerivce.UpdateBatchStatus(requestId, BatchStatusesKeys.CALL_VERIFIED, UserName);

                if (!updateBatch)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                return Ok(updateBatch);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<PendingBatchesController>(e.Message, e, requestId, nameof(UpdateBatchStatus));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Set specific Batch In Call.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("SetBatchInCall/{requestId}")]
        public async Task<IActionResult> SetBatchInCall(string requestId)
        {
            try
            {
                var batch = await _batchSerivce.GetBatchByToken(requestId);
                if (batch == null)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                var aspNetUserId = _batchSerivce.GetAspnetUser(UserId);

                if (batch.LockedBy != null && batch.LockedBy != aspNetUserId)
                    return BadRequest(MsgKeys.NOK_LockedByOtherAgent);
                else
                {
                    var updateStatus = await _batchSerivce.UpdateBatchStatus(batch.RequestId, BatchStatusesKeys.IN_CALL, UserName);
                    if (updateStatus)
                        return Ok(MsgKeys.BATCHSET);
                }
                return BadRequest(MsgKeys.InvalidInputParameters);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<PendingBatchesController>(e.Message, e, requestId, nameof(SetBatchInCall));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Get Batch Item Verification Results.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("GetBatchItemVerificationResults/{batchItemId}")]
        public async Task<IActionResult> GetBatchItemVerificationResults(int batchItemId)
        {
            try
            {
                var batchItemFields = await _batchSerivce.GetBatchItemFields(batchItemId);

                return Ok(new
                {
                    Items = batchItemFields
                });
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError(e, "{0}", e.Message);

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Get Batch Item Document Base64 String By Name.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("GetBatchItemDocumentBase64StringByName/{token}/{filename}")]
        public IActionResult GetBatchItemDocumentBase64StringByName(string token, string fileName)
        {
            try
            {
                Batch batch = _batchSerivce.LoadBatchForBatchItemDocuments(token);

                var pageOfInterest = batch.BatchItems.SelectMany(bi => bi.BatchItemPages).FirstOrDefault(p => p.FileName == fileName);

                if (pageOfInterest == null)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                var batchOutputPath = _bopConfigService.GetBopConfigByEnumValue(BopConfigs.BATCH_FILES_NETWORK_PATH);

                var batchDir = Path.Combine(batchOutputPath, batch.CreatedDate.IsoYearMonth(true), batch.CreatedDate.IsoDate(true), batch.Id.ToString());

                if (!Directory.Exists(batchDir))
                    Directory.CreateDirectory(batchDir);

                string filePath = Path.Combine(batchDir, pageOfInterest.FileName);

                if (!System.IO.File.Exists(filePath))
                    return BadRequest(MsgKeys.PathNotExist);

                string base64String = Convert.ToBase64String(System.IO.File.ReadAllBytes(filePath.ToString()));

                return Ok(base64String);
            }
            catch (Exception e)
            {

                // Log error message
                _logger.LogError<PendingBatchesController>(e.Message, e, token, nameof(GetBatchItemDocumentBase64StringByName));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }
        
        /// <summary>
        /// verify batch for agent index.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("RequestPdfDownloadAccess/{requestId}")]
        public async Task<IActionResult> RequestPdfDownloadAccess(string requestId)
        {
            try
            {
                var pdfLinks = await _batchSerivce.RequestDocumentsAccess(requestId);
                if (pdfLinks == null || pdfLinks.Count < 1)
                    return BadRequest(MsgKeys.NoFileFound);
                else
                    return Ok(pdfLinks);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<PendingBatchesController>(e.Message, e, requestId, nameof(RequestPdfDownloadAccess));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }


    }
}
