using AutoMapper;
using Intelli.AgentPortal.Api.DTO;
using Intelli.AgentPortal.Api.Helpers;
using Intelli.AgentPortal.Domain.Core.Helpers;
using Intelli.AgentPortal.Domain.Core.Repository;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Domain.Repository;
using Intelli.AgentPortal.Domain.Repository.Impl;
using Intelli.AgentPortal.EventBus.RabbitMQ.Event;
using Intelli.AgentPortal.EventBus.RabbitMQ.Sender;
using Intelli.AgentPortal.Shared;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Intelli.AgentPortal.Shared.Mvc.DocumentClassFields;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class BatchController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICompanyRepository<Batch> _repositoryBatch;
        private readonly ICompanyRepository<AspNetReseller> _repositoryResellers;
        private readonly ICustomRepository<BatchMetum> _repositoryBatchMetum;
        private readonly ICustomRepository<BatchHistory> _repositoryBatchHistory;
        private readonly ICustomRepository<BatchStatus> _repositoryBatchStatus;
        private readonly IDocumentClassFields _documentClassFieldService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="sender">The Event Sender.</param>
        public BatchController(AgentPortalContext context, IMapper mapper,
            ILogger<BatchController> logger,
            IEventSender sender,
            IDocumentClassFields documentClassFieldService)
        {
            _repositoryBatch = new CompanyEntityRepository<Batch>(context);
            _repositoryBatchMetum = new CustomRepository<BatchMetum>(context);
            _repositoryBatchHistory = new CustomRepository<BatchHistory>(context);
            _repositoryBatchStatus = new CustomRepository<BatchStatus>(context);
            _repositoryResellers = new CompanyEntityRepository<AspNetReseller>(context);

            ((CompanyEntityRepository<Batch>)_repositoryBatch).AfterSave =
            ((CustomRepository<BatchMetum>)_repositoryBatchMetum).AfterSave = (logs) =>
                sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

            _logger = logger;
            _mapper = mapper;
            _documentClassFieldService = documentClassFieldService;
        }

        /// <summary>
        /// Gets the batches.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns>ActionResult of IEnumerable of Role</returns>
        [HttpGet]
        public ActionResult<IEnumerable<BatchDTO>> GetAll([FromQuery] QueryStringParams queryStringParams)
        {
            _logger.LogInformation("GetBatches Called with params: {0}", queryStringParams);
            PagedResult<BatchDTO> result = null;
            try
            {
                QueryResult<Batch> queryResult = new();
                if (!queryStringParams.IsExport)
                {
                    queryResult = _repositoryBatch.Get(CompanyIds,
                                    queryStringParams.FilterExpression,
                                    queryStringParams.OrderBy,
                                    queryStringParams.PageSize,
                                    queryStringParams.PageNumber,
                                    x => x.Company,
                                    g => g.BatchStatus);
                }
                else
                {
                    queryResult = _repositoryBatch.Get(CompanyIds,
                                    queryStringParams.FilterExpression,
                                    queryStringParams.OrderBy,
                                    x => x.Company,
                                    g => g.BatchStatus);
                }

                int total = queryResult.Count;
                var batchList = queryResult.List;

                //List<Country> agentSpeakingLanguages = new List<Country>();
                //List<BatchMetum> metaList = new List<BatchMetum>();
                //List<int> constantFieldIds = new List<int>();

                //if (UserId != 0)
                //{
                //    var query = _context.Countries.FromSqlInterpolated(
                //          $"EXEC dbo.sp_GetUserLanguages2 {UserId}");
                //    agentSpeakingLanguages = query.ToList();
                //}

                //filtering metalist on the basis of user language
                //if (agentSpeakingLanguages.Count > 0)
                //{
                //     metaList = GetBatchMetaList(batchList, agentSpeakingLanguages);
                //   // var batchIdList = batchList.Select(d => d.Id).ToList();
                //   // var fieldIdList = _repositoryBatchMetum.Query(c => batchIdList.Contains(c.BatchId)).Select(c=>c.DocumentClassFieldId).Distinct().ToList();
                //   // constantFieldIds = _documentClassFieldService.GetDocumentClassFieldValues(fieldIdList, true);

                //   // batchList = batchList.Where(d =>
                //   //constantFieldIds.Contains(d.BatchMeta.Select(b => b.DocumentClassFieldId))).ToList();
                //   // //  d.BatchMeta.Any(b => agentSpeakingLanguages.Select(x => x.Code2D).Contains(b.FieldValue)) ||
                //      //  d.BatchMeta.Any(b => agentSpeakingLanguages.Select(x => x.Code3D).Contains(b.FieldValue))).ToList();
                //}
                //else
                //{
                //    metaList = GetBatchMetaList(batchList, null);
                //}

                //batchList.ForEach(s => s.BatchMeta = metaList
                //.Where(d => d.BatchId == s.Id).ToList());

                result = new PagedResult<BatchDTO>(
                        total,
                        queryStringParams.PageNumber,
                        batchList.Select(x =>
                        {
                            var dto = _mapper.Map<BatchDTO>(x);
                            dto.BatchStatus = _mapper.Map<BatchStatusDTO>(x.BatchStatus);
                            dto.Company = _mapper.Map<BatchCompaniesDTO>(x.Company);
                            dto.BatchMeta = x.BatchMeta.Select(y => _mapper.Map<BatchMetumDTO>(y)).ToList();
                            return dto;

                        }).ToList(),
                        queryStringParams.PageSize
                    );
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
            return Ok(result);
        }

        /// <summary>
        /// Get Batch meta list of specified language.
        /// </summary>
        /// <returns>List of BatchMetum.</returns>
        private List<BatchMetum> GetBatchMetaList(List<Batch> batchList, List<Country> languages)
        {
            var batchIdList = batchList.Select(d => d.Id).ToList();
            var batchMetaList = _repositoryBatchMetum.Query(c => batchIdList.Contains(c.BatchId)).ToList();

            List<int> constantFieldIds = new List<int>();

            if (languages != null)
            {
                constantFieldIds = _documentClassFieldService.GetDocumentClassFieldValues(batchMetaList.Select(d => d.DocumentClassFieldId).Distinct().ToList(), true);

                batchList = batchList.Where(d =>
                 d.BatchMeta.Any(b => constantFieldIds.Contains(b.DocumentClassFieldId)) &&
                  d.BatchMeta.Any(b => languages.Select(x => x.Code2D).Contains(b.FieldValue)) ||
                    d.BatchMeta.Any(b => languages.Select(x => x.Code3D).Contains(b.FieldValue))).ToList();

                batchMetaList = batchMetaList.Where(d =>
                constantFieldIds.Contains(d.DocumentClassFieldId) &&
                (languages.Select(x => x.Code2D).Contains(d.FieldValue) ||
                    languages.Select(x => x.Code3D).Contains(d.FieldValue))).ToList();
            }
            else
            {
                constantFieldIds = _documentClassFieldService.GetDocumentClassFieldValues(batchMetaList.Select(d => d.DocumentClassFieldId).Distinct().ToList(), false);
                batchMetaList = batchMetaList.Where(d =>
                constantFieldIds.Contains(d.DocumentClassFieldId)).ToList();
            }
            return batchMetaList;
        }

        /// <summary>
        /// Get all active users.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetActive()
        {
            try
            {
                var dto = new BatchDTO();
                var batchList = await _repositoryBatch.Query(CompanyIds).
                    Include(s => s.Company).
                    Include(z => z.BatchStatus).
                    ToListAsync();

                return Ok(new
                {
                    Items = batchList.Select(x =>
                    {
                        var dto = _mapper.Map<BatchDTO>(x);
                        dto.BatchStatus = _mapper.Map<BatchStatusDTO>(x.BatchStatus);
                        dto.Company = _mapper.Map<BatchCompaniesDTO>(x.Company);
                        dto.BatchMeta = x.BatchMeta.Select(y => _mapper.Map<BatchMetumDTO>(y)).ToList();
                        return dto;

                    }).ToList(),
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
        /// Get all Batch Statuses.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("batchStatuses")]
        public async Task<IActionResult> GetBatchStatuses()
        {
            try
            {
                var query = _repositoryBatchStatus.Query();
                var batchStatusList = await query.ToListAsync();

                return Ok(new { items = batchStatusList.Select(x => _mapper.Map<BatchStatusDTO>(x)) });
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
        /// Get all Batch Histories.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("batchHistories/{requestId}")]
        public async Task<IActionResult> GetBatchHistories(string requestId)
        {
            try
            {
                var query = _repositoryBatchHistory.Query(d => d.Batch.RequestId == requestId);
                var batchHistoryList = await query.ToListAsync();

                return Ok(new { items = batchHistoryList.Select(x => _mapper.Map<BatchHistoriesDTO>(x)) });

            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError<BatchController>(e.Message, e, requestId, nameof(GetBatchHistories));

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }
    }
}
