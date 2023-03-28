using System.ComponentModel.DataAnnotations;

namespace Intelli.AgentPortal.Api.DTO
{
    /// <summary>
    /// The screen element DTO.
    /// </summary>
    public class ScreenElementDTO
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the screen id.
        /// </summary>
        public int ScreenId { get; set; }

        /// <summary>
        /// Gets or sets the Screen Element Name.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ScreenElementName { get; set; }
    }
}
