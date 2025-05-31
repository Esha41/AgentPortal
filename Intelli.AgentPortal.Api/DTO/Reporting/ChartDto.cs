using System.Collections.Generic;

namespace Intelli.AgentPortal.Api.DTO.Reporting
{
    /// <summary>
    /// The chart dto.
    /// </summary>
    public class ChartDto
    {
        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        public List<string> Labels { get; set; }

        /// <summary>
        /// Gets or sets the datasets.
        /// </summary>
        public List<ChartDatasetDto> Datasets { get; set; }
    }
}
