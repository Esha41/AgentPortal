using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Intelli.AgentPortal.Api.DTO;
using Intelli.AgentPortal.Api.Helpers;
using Intelli.AgentPortal.Api.Services.Batches;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.EventBus.RabbitMQ.Sender;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Intelli.AgentPortal.Shared.Mvc.DocumentClassFields;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Controllers.Batches
{
    /// <summary>
    /// The roles controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerVideoScreenController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IBatchService _batchSerivce;
        private readonly IDocumentClassFields _documentClassFieldService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerVideoScreenController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="sender">The Event Sender.</param>'
        public CustomerVideoScreenController(AgentPortalContext context, IMapper mapper,
          ILogger<CustomerVideoScreenController> logger,
          IEventSender sender,
          IBatchService batchSerivce,
            IDocumentClassFields documentClassFieldService)
        {
            _documentClassFieldService = documentClassFieldService;
            _logger = logger;
            _mapper = mapper;
            _batchSerivce = batchSerivce;
        }

        /// <summary>
        /// Get Uploaded Documents.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("UploadedDocuments/{requestId}")]
        public async Task<IActionResult> UploadedDocuments(string requestId)
        {
            AgentViewDTO agentViewModel = new AgentViewDTO();
            List<BatchItemVMDTO> batchItems = new List<BatchItemVMDTO>();
            try
            {
                //get batch by request id
                var batch = await _batchSerivce.GetBatchByToken(requestId);

                if (batch == null)
                    return BadRequest(new { Message = MsgKeys.InvalidInputParameters });

                // Get Information From DB using sp_GetPage_AgentVideo
                var agentVideoPage = _batchSerivce.GetAgentVideoPage(requestId);

                // Batch Items extraction here
                foreach (var batchItem in agentVideoPage.batchItemsViewModel)
                    batchItems.Add(new BatchItemVMDTO(batchItem, requestId));

                //Check if all needed documents are uploaded
                var batchUploadedDocuments = agentVideoPage.batchHistoryItems.GroupBy(l => l.DocumentGroupNameId).Count();
                var batchNeedDocuments = agentVideoPage.batchSourceUploadDocs.Count();

                agentViewModel.BatchHistoryItems = agentVideoPage.batchHistoryItems;
                agentViewModel.BatchItems = batchItems;
                agentViewModel.Token = batch.Token;
                agentViewModel.batch = _mapper.Map<FullBatchDto>(batch);
                agentViewModel.AllUploadedDocumentsExist = (batchUploadedDocuments == batchNeedDocuments) ? true : false;

                return Ok(agentViewModel);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<CustomerVideoScreenController>(e.Message, e, requestId, nameof(UploadedDocuments));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Get VerificationResults.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("VerificationResults/{requestId}")]
        public async Task<IActionResult> VerificationResults(string requestId)
        {
            try
            {
                var verificationRejectionReasons = new List<string>();
                var RejectionReasonsCode = new List<string>();
                AgentViewDTO agentViewModel = new AgentViewDTO();

                var batch = await _batchSerivce.GetBatchByToken(requestId);
                if (batch == null)
                    return BadRequest(new { Message = MsgKeys.InvalidInputParameters });

                // Get Information From DB using sp_GetPage_AgentVideo
                var agentVideoPage = _batchSerivce.GetAgentVideoPage(requestId);
                RejectionReasonsCode = agentVideoPage.VerificationRejectionReason?.Split(',').Select(x => x.Trim()).ToList();

                if (RejectionReasonsCode != null && RejectionReasonsCode.Count>0)
                {

                    foreach (var item in RejectionReasonsCode)
                    {
                        var rejectionCode = item.Split(',');
                        string finalString = "";
                        List<string> rejectReasonList = new List<string>();

                        foreach (var tempItem in rejectionCode)
                        {
                            var rejectReason = _documentClassFieldService.GetRejectionReasonFromCode(tempItem.Trim());
                            rejectReasonList.Add(rejectReason);
                        }

                        finalString = string.Join(",", rejectReasonList.ToArray());
                        if (String.IsNullOrEmpty(finalString))
                            continue;
                        verificationRejectionReasons.Add(finalString);
                    }

                }

                agentViewModel.Token = batch.Token;
                agentViewModel.batch = _mapper.Map<FullBatchDto>(batch);
                agentViewModel.VerificationRejectionReasons = verificationRejectionReasons != null ? verificationRejectionReasons.Select(x => x.Trim()).Distinct().ToList() : null;
                agentViewModel.FaceMatching = agentVideoPage.FaceMatching ?? "";
                agentViewModel.IsAlive = agentVideoPage.IsAlive ?? "";
                agentViewModel.VerificationStatus = agentVideoPage.VerificationStatus.ToString();

                return Ok(agentViewModel);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<CustomerVideoScreenController>(e.Message, e, requestId, nameof(VerificationResults));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Get Uploaded Documents For Agent Index.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("UploadedDocumentsForAgentIndex/{requestId}")]
        public async Task<IActionResult> UploadedDocumentsForAgentIndex(string requestId)
        {
            try
            {
                var agentViewModel = await _batchSerivce.GetUploadedDocumentsForAgentIndex(requestId);
                if (agentViewModel == null)
                    return BadRequest(new { Message = MsgKeys.InvalidInputParameters });

                return Ok(agentViewModel);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<CustomerVideoScreenController>(e.Message, e, requestId, nameof(UploadedDocuments));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Get Verification Results For Agent Index.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("VerificationResultsForAgentIndex/{requestId}")]
        public async Task<IActionResult> VerificationResultsForAgentIndex(string requestId)
        {
            try
            {
                var agentViewModel = await _batchSerivce.GetVerificationResultsForAgentIndex(requestId);
                if (agentViewModel == null)
                    return BadRequest(new { Message = MsgKeys.InvalidInputParameters });

                return Ok(agentViewModel);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<CustomerVideoScreenController>(e.Message, e, requestId, nameof(VerificationResults));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Get Signed Documents For Agent Index.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("SignedDocumentsForAgentIndex/{requestId}")]
        public async Task<IActionResult> SignedDocumentsForAgentIndex(string requestId)
        {
            try
            {
                var agentViewModel = await _batchSerivce.GetSignedDocumentsForAgentIndex(requestId);
                if (agentViewModel == null)
                    return BadRequest(new { Message = MsgKeys.InvalidInputParameters });

                return Ok(agentViewModel);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<CustomerVideoScreenController>(e.Message, e, requestId, nameof(VerificationResults));

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
        [Route("ByPassSynchordiaVerification/{requestId}")]
        public async Task<IActionResult> ByPassSynchordiaVerification(string requestId)
        {
            try
            {
                var updateBatch = await _batchSerivce.UpdateBatchForAgentIndex(requestId, UserName);
                if (!updateBatch)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                return Ok(updateBatch);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<CustomerVideoScreenController>(e.Message, e, requestId, nameof(ByPassSynchordiaVerification));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
            //var batch = serviceFactory.BatchService.GetBatchBySRID(srid);
            //var lastBatchHistory = serviceFactory.BatchService.GetBatchHistoryByBatchId(batch.Id.ToString());

            //// bypass if only it has result
            //if (lastBatchHistory?.ResponseDate != null)
            //{
            //    serviceFactory.BatchService.AgentUpdateBatchHistory(batch.Id.ToString(), agentid);

            //    // SignalR notify here all Portal Servers
            //    var context = GlobalHost.ConnectionManager.GetHubContext<BatchHistoryHub>();

            //    var clients = context.Clients.All;

            //    var test = context.Clients.All.sendMessage(batch.Token);

            //    var tests = BatchHistoryHub.ConnectedUser.Ids.ToList();

            //    return Request.CreateResponse(HttpStatusCode.OK, true);
            //}
            //else
            //{
            //    return Request.CreateResponse(HttpStatusCode.OK, false);
            //}

        }

        /// <summary>
        ///Upload Documents For AgentJumio.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("UploadedDocumentsForAgentJumio/{requestId}")]
        public async Task<IActionResult> UploadedDocumentsForAgentJumio(string requestId)
        {
            try
            {
                var agentViewModel = await _batchSerivce.GetUploadedDocumentsForAgentJumio(requestId);
                if (agentViewModel == null)
                    return BadRequest(new { Message = MsgKeys.InvalidInputParameters });

                return Ok(agentViewModel);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<CustomerVideoScreenController>(e.Message, e, requestId, nameof(UploadedDocuments));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        ///Verification Results For AgentJumio.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("VerificationResultsForAgentJumio/{requestId}")]
        public async Task<IActionResult> VerificationResultsForAgentJumio(string requestId)
        {
            try
            {
                var agentViewModel = await _batchSerivce.GetVerificationResultsForAgentJumio(requestId);
                if (agentViewModel == null)
                    return BadRequest(new { Message = MsgKeys.InvalidInputParameters });

                return Ok(agentViewModel);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<CustomerVideoScreenController>(e.Message, e, requestId, nameof(VerificationResults));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        ///Get Verification Txn Data for edit verification .
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <param name="batchItemId">The batchItemId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("GetVerificationTxnData/{requestId}/{batchItemId}")]
        public async Task<IActionResult> GetVerificationTxnData(string requestId, int batchItemId)
        {
            try
            {
                var getVerificationResult = await _batchSerivce.GetVerificationTxnData(requestId, batchItemId);

                // Checking if the passed parameter is valid
                if (getVerificationResult == null)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                return Ok(getVerificationResult);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<CustomerVideoScreenController>(e.Message, e, requestId, nameof(GetVerificationTxnData));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        ///Get Verification Data for edit verification .
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <param name="batchHistoryItemId">The batchHistoryItemId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("GetEditVerificationData/{requestId}/{batchHistoryItemId}")]
        public async Task<IActionResult> GetEditVerificationData(string requestId, int batchHistoryItemId)
        {
            try
            {
                var getVerificationResult = await _batchSerivce.GetEditVerificationResult(requestId, batchHistoryItemId);

                // Checking if the passed parameter is valid
                if (getVerificationResult == null)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                return Ok(getVerificationResult);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<CustomerVideoScreenController>(e.Message, e, requestId, nameof(GetEditVerificationData));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Edit Verification Txn Data
        /// </summary>
        /// <returns>An ActionResult.</returns>
        [HttpPost]
        [Route("EditVerificationTxnData")]
        public IActionResult EditVerificationTxnData(List<EditTxnVerificationResultDTO> updatedFields)
        {
            try
            {
                // Checking if the passed DTO is valid
                if (!ModelState.IsValid || updatedFields == null || updatedFields.Count() == 0)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                if (_batchSerivce.UpdateBatchItemFields(updatedFields, UserName))
                    return Ok(MsgKeys.UpdatedSuccessfully);

                return BadRequest(MsgKeys.InvalidInputParameters);
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
        /// Edit Verification Data
        /// </summary>
        /// <returns>An ActionResult.</returns>
        [HttpPost]
        [Route("EditVerificationResults")]
        public IActionResult EditVerificationResults(List<EditVerificationResultDTO> updatedFields)
        {
            try
            {
                // Checking if the passed DTO is valid
                if (!ModelState.IsValid || updatedFields == null || updatedFields.Count() == 0)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                if (_batchSerivce.UpdateBatchHistoryItemField(updatedFields, UserName))
                    return Ok(MsgKeys.UpdatedSuccessfully);

                return BadRequest(MsgKeys.InvalidInputParameters);
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
        /// Get Batch History Item By Id.
        /// </summary>
        /// <param name="batchHistoryItemId">The batchHistoryItemId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("GetBatchHistoryItemById/{batchHistoryItemId}")]
        public IActionResult GetBatchHistoryItemById(int batchHistoryItemId)
        {
            try
            {
                var getVerificationResult = _batchSerivce.GetBatchHistoryItemById(batchHistoryItemId);

                // Checking if the passed parameter is valid
                if (getVerificationResult == null)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                return Ok(getVerificationResult);
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

    }
}
