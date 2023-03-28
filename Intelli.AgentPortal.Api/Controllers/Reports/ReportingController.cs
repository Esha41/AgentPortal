using Intelli.AgentPortal.Api.DTO.Reporting;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model.StoredProceduresOutput;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Controllers.v1
{
    /// <summary>
    /// The Reporting Controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportingController : BaseController
    {
        private readonly AgentPortalContext _context;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="AgentPortalContext"/> will be injected</param>
        public ReportingController(AgentPortalContext context, ILogger<ReportingController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Returns batches count report.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>A Reports collection response type.</returns>
        [HttpPost]
        public async Task<IActionResult> BatchesCount([FromBody] BatchesCountDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(MsgKeys.InvalidInputParameters);

                //var createDateFrom = DateTimeOffset.FromUnixTimeSeconds(dto.CreateDateFrom).DateTime.ToString("yyyy-MM-dd");
                //var createDateTo = DateTimeOffset.FromUnixTimeSeconds(dto.CreateDateTo).DateTime.ToString("yyyy-MM-dd");

                var query = _context.BatchesCount.FromSqlInterpolated(
                                $"EXEC dbo.batches_count {dto.CompanyId}, {dto.DateFrom}, {dto.DateTo}");

                var list = await query.ToListAsync();

                var graphDto = GetGraphData(list);

                return Ok(graphDto);
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError(e, "{0}", e.Message);

                return null;
            }
        }

        /// <summary>
        /// Gets the graph data.
        /// </summary>
        /// <param name="list">The list of batches result.</param>
        /// <returns>A GraphDto.</returns>
        private static ChartDto GetGraphData(List<BatchesCount> list)
        {
            var dto = new ChartDto
            {
                Labels = new List<string>(),
                Datasets = new List<ChartDatasetDto>
                {
                    new ChartDatasetDto
                    {
                        Label = "Batches",
                        Data = new List<int>(),
                    }
                }
            };

            foreach (var item in list)
            {
                dto.Labels.Add(item.CreatedDate);
                dto.Datasets[0].Data.Add(item.Count);
            }

            return dto;
        }
    }
}
