using AutoMapper;
using Intelli.AgentPortal.Api.DTO;
using Intelli.AgentPortal.Api.Helpers;
using Intelli.AgentPortal.Api.Services.Batches;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Intelli.AgentPortal.Shared.Mvc.DocumentClassFields;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
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
    public class CustomerDataScreenController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IBatchService _batchSerivce;
        private readonly IDocumentClassFields _documentClassFieldService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerDataScreenController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        public CustomerDataScreenController(IMapper mapper,
            ILogger<CustomerDataScreenController> logger,
            IBatchService batchSerivce,
              IDocumentClassFields documentClassFieldService)
        {
            _documentClassFieldService = documentClassFieldService;
            _logger = logger;
            _mapper = mapper;
            _batchSerivce = batchSerivce;
        }

        /// <summary>
        /// Get company specific Customer Data.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("customerData/{requestId}")]
        public IActionResult GetCustomerData(string requestId)
        {
            try
            {
                var batch = _batchSerivce.LoadFullBatch(requestId, UserId, true);
                if (batch == null)
                    return Ok(MsgKeys.NotAllowed);

                List<CustomerData> customerInfo = _batchSerivce.GetCustomerData(batch);

                return Ok(new
                {
                    Items = customerInfo
                });
            }
            catch(Exception e)
            {
                // Log error message
                _logger.LogError<CustomerDataScreenController>(e.Message, e, requestId, nameof(GetCustomerData));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
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
            var batchHistoryItemsVw = new List<VwDocumentGroupNamesFormBatchHistory>();
            var batchHistoryItems = new List<DboVwDocumentGroupNamesForBatchHistory>();
            var batchSourceUploadDocs = new List<BatchSourceUploadDocDTO>();
            var signedDocuments = new List<UploadedDocumentsDTO>();
            UploadedDocumentsViewDTO uploadedDocumentsList = new UploadedDocumentsViewDTO();
            List<BatchItemVMDTO> batchItems = new List<BatchItemVMDTO>();
            List<BatchHistory> batchHistories = new List<BatchHistory>();

            try
            {
                //get batch by request id
                var batch = await _batchSerivce.GetBatchByToken(requestId);

                if (batch == null)
                    return BadRequest(new { Message = MsgKeys.InvalidInputParameters });

                //get batch history by batch id
                batchHistories = _batchSerivce.GetBatchHistories(batch.Id);

                batchSourceUploadDocs = _batchSerivce.BatchSourceUploadDoc(batch.BatchSourceId);
                signedDocuments = _batchSerivce.GetAllSignedDocuments(batch);

                //get batch history items by batch history ids
                var batchHistoryIds = batchHistories.Select(d => d.Id).ToList();
                var batchHistoryItems_ = _batchSerivce.GetBatchHistorItems(batchHistoryIds);
                batchHistoryItemsVw.AddRange(batchHistoryItems_);

                if (batchHistoryItemsVw != null)
                {
                    foreach (var item in batchHistoryItemsVw)
                    {
                        var batchHistoryItemObj = new DboVwDocumentGroupNamesForBatchHistory()
                        {
                            Id = item.Id,
                            BatchHistoryId = item.BatchHistoryId,
                            RegisterDate = item.RegisterDate,
                            DocumentGroupName = item.DocumentGroupName,
                            IsValid = item.IsValid,
                            FileName = item.FileName,
                            PageId = item.PageId,
                            DocumentGroupNameId = item.DocumentGroupNameId,
                            IsLast = item.IsLast
                        };
                        batchHistoryItems.Add(batchHistoryItemObj);
                    }
                }

                var batchUploadedDocuments = batchHistoryItems.GroupBy(l => l.DocumentGroupNameId).Count();
                var batchNeedDocuments = batchSourceUploadDocs.Count();

                // Get Information From DB using sp_GetPage_AgentVideo
                var agentVideoPage = _batchSerivce.GetAgentVideoPage(requestId);
                // Batch Items extraction here
                foreach (var bi in agentVideoPage.batchItemsViewModel)
                    batchItems.Add(new BatchItemVMDTO(bi, requestId));

                //filling dto
                uploadedDocumentsList.Token = batch.Token;
                uploadedDocumentsList.AllUploadedDocumentsExist = (batchUploadedDocuments == batchNeedDocuments) ? true : false;
                uploadedDocumentsList.BatchHistoryItems = agentVideoPage.batchHistoryItems ?? new List<DboVwDocumentGroupNamesForBatchHistory>();
                uploadedDocumentsList.SignedDocuments = signedDocuments ?? new List<UploadedDocumentsDTO>();
                uploadedDocumentsList.BatchItems = batchItems;

                return Ok(new
                {
                    Items = uploadedDocumentsList
                });
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<CustomerDataScreenController>(e.Message, e, requestId, nameof(UploadedDocuments));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Get Verification Results.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("VerificationResults/{requestId}")]
        public async Task<IActionResult> VerificationResults(string requestId)
        {
            try
            {
                var batch = await _batchSerivce.GetBatch(requestId);
                if (batch == null)
                    return BadRequest(new { Message = MsgKeys.InvalidInputParameters });

                var rejectionReasonsFromBatchMeta = new List<string>();
                var verificationRejectionReasons = new List<string>();
                string faceMatching = null;
                string isAlive = null;
                string verificationStatus = null;

                try
                {
                    var documentClassFieldId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.SimilarityByAgent).Id;
                    faceMatching = batch.BatchMeta.Where(bm => bm.DocumentClassFieldId == documentClassFieldId)?.FirstOrDefault()?.FieldValue;
                }
                catch (Exception)
                {
                    faceMatching = null;
                }

                try
                {
                    var documentClassFieldId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.IsAlive).Id;
                    isAlive = batch.BatchMeta.Where(bm => bm.DocumentClassFieldId == documentClassFieldId)?.FirstOrDefault()?.FieldValue;
                }
                catch (Exception)
                {
                    isAlive = null;
                }

                try
                {
                    var documentClassFieldId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.VerificationStatus).Id;
                    verificationStatus = batch.BatchMeta.Where(bm => bm.DocumentClassFieldId == documentClassFieldId)?.FirstOrDefault()?.FieldValue;
                }
                catch (Exception)
                {
                    verificationStatus = null;
                }

                try
                {
                    var documentClassFieldId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.VerificationRejectionReason).Id;
                    rejectionReasonsFromBatchMeta = batch.BatchMeta.Where(bm => bm.DocumentClassFieldId == documentClassFieldId)?.FirstOrDefault()?.FieldValue.Split('|').Select(x => x.Trim()).ToList();
                }
                catch (Exception)
                {
                    rejectionReasonsFromBatchMeta = null;
                }
                if (rejectionReasonsFromBatchMeta != null)
                {

                    foreach (var item in rejectionReasonsFromBatchMeta)
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
                VerificationResultsDTO dto = new VerificationResultsDTO
                {
                    Token = batch.Token,
                    Batch = _mapper.Map<BatchDTO>(batch),
                    VerificationRejectionReasons = verificationRejectionReasons != null ? verificationRejectionReasons.Select(x => x.Trim()).Distinct().ToList() : null,
                    FaceMatching = faceMatching ?? "",
                    IsAlive = isAlive ?? "",
                    VerificationStatus = verificationStatus
                };

                return Ok(dto);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<CustomerDataScreenController>(e.Message, e, requestId, nameof(VerificationResults));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Get Verification Results on refresh button.
        /// </summary>
        /// <param name="batchHistoryItemId">The batchHistoryItemId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("GetVerificationResults/{batchHistoryItemId}")]
        public async Task<IActionResult> GetVerificationResults(int batchHistoryItemId)
        {
            try
            {
                var batchHistoryItemFields =await _batchSerivce.GetBatchHistoryItemFields(batchHistoryItemId);

                foreach (var item in batchHistoryItemFields)
                {
                    if (item.Uilabel.Contains("RejectionReason"))
                    {
                        var rejectionCode = item.RegisteredFieldValue.Split(',');
                        string finalString = "";
                        List<string> rejectReasonList = new List<string>();

                        foreach (var tempItem in rejectionCode)
                        {
                            var rejectReason = _documentClassFieldService.GetRejectionReasonFromCode(tempItem.Trim());
                            rejectReasonList.Add(rejectReason);
                        }

                        finalString = string.Join(",", rejectReasonList.ToArray());
                        item.RegisteredFieldValue = finalString;
                    }
                }

                return Ok(new
                {
                    Items = batchHistoryItemFields
                });
            }
            catch(Exception e)
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
        /// Get Document Base 64String By Name.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="pageId">The pageId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("GetDocumentBase64StringByName/{token}/{pageId}")]    
        public async Task<IActionResult> GetDocumentBase64StringByName(string token, string pageId) 
        {
            try
            {
                var base64String =await _batchSerivce.GetDocumentBase64StringbyPageId(token, pageId);

                if (base64String == null)
                    return BadRequest(new { Message = MsgKeys.InvalidInputParameters });

                return Ok(base64String);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<CustomerDataScreenController>(e.Message, e, token, nameof(GetDocumentBase64StringByName));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Get Signed Documents.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("SignedDocuments/{requestId}")]
        public async Task<IActionResult> SignedDocuments(string requestId)
        {
            try
            {
                var batch =await _batchSerivce.GetBatchByToken(requestId);
                if (batch == null)
                    return BadRequest(new { Message = MsgKeys.InvalidInputParameters });

                var signedDocuments = new List<UploadedDocumentsDTO>();
                signedDocuments = _batchSerivce.GetAllSignedDocuments(batch);

                SignedDocumentsDTO dto = new SignedDocumentsDTO
                {
                    Batch = _mapper.Map<BatchDTO>(batch),
                    SignedDocuments = signedDocuments ?? new List<UploadedDocumentsDTO>()
                };
                return Ok(dto);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<CustomerDataScreenController>(e.Message, e, requestId, nameof(SignedDocuments));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        ///  Get Base 64String For Image.
        /// </summary>
        /// <param name="imgPath">The imgPath.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("GetBase64StringForImage/{imgPath}")]
        public IActionResult GetBase64StringForImage(string imgPath)
        {
            try
            {
                var imageBytes = System.IO.File.ReadAllBytes(imgPath);
                var base64String = Convert.ToBase64String(imageBytes);
                return Ok(base64String);
            }
            catch(Exception e)
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
