using System.Collections.Generic;

namespace Intelli.AgentPortal.Api.DTO.Reporting
{
    /// <summary>
    /// The chart dataset dto.
    /// </summary>
    public class ChartDatasetDto
    {
        /// <summary>
        /// Gets or sets the chart dataset label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public List<int> Data { get; set; }
    }
}