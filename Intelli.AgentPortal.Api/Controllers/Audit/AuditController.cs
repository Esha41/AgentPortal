using Intelli.AgentPortal.Domain.Core.Repository;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Intelli.AgentPortal.Api.Controllers.v1
{
    /// <summary>
    /// The Audit api controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuditController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IRepository<Audit> _auditRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditController"/> class.
        /// </summary>
        /// <param name="context">The db context.</param>
        /// <param name="logger">The logger.</param>
        public AuditController(AgentPortalAuditContext context, ILogger<AuditController> logger)
        {
            _auditRepository = new GenericRepository<Audit>(context);
            _logger = logger;
        }

        /// <summary>
        /// Gets the Audit.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns>ActionResult of Response object.</returns>
        [HttpGet]
        public ActionResult<Response> GetAudits([FromQuery] QueryStringParams queryStringParams)
        {
            _logger.LogInformation("Get Audit log Called with params: {0}", queryStringParams);
            PagedResult<Audit> result;

            try
            {
                QueryResult<Audit> queryResult = new();
                if (!queryStringParams.IsExport)
                {
                    queryResult = _auditRepository.Get(
                                    queryStringParams.FilterExpression,
                                    queryStringParams.OrderBy,
                                    queryStringParams.PageSize,
                                    queryStringParams.PageNumber );
                }
                else
                {
                    queryResult = _auditRepository.Get(
                                    queryStringParams.FilterExpression,
                                    queryStringParams.OrderBy);
                }

                int total = queryResult.Count;
                var auditList = queryResult.List;

                result = new PagedResult<Audit>(
                        total,
                        queryStringParams.PageNumber,
                        auditList,
                        queryStringParams.PageSize
                    );
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { Errors = e, e.Message });
            }

            return Ok(result);
        }
    }
}
