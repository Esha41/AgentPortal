using System.ComponentModel.DataAnnotations;

namespace Intelli.AgentPortal.Api.DTO.Reporting
{
    /// <summary>
    /// The batches count report request dto.
    /// </summary>
    public class BatchesCountDto
    {
        /// <summary>
        /// Gets or sets the company id.
        /// </summary>
        [Required]
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the create date from.
        /// </summary>
        [Required]
        public long CreateDateFrom { get; set; }

        /// <summary>
        /// Gets or sets the create date to.
        /// </summary>
        [Required]
        public long CreateDateTo { get; set; }

        public string DateFrom { get; set; }

        /// <summary>
        /// Gets or sets the create date to.
        /// </summary>

        public string DateTo { get; set; }
    }
}