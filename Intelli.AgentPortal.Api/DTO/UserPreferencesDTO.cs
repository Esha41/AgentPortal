using System.ComponentModel.DataAnnotations;

namespace Intelli.AgentPortal.Api.DTO
{
    /// <summary>
    /// The user preferences DTO.
    /// </summary>
    public class UserPreferencesDTO
    {
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        [Required]
        [MaxLength(5)]
        public string Language { get; set; } = "en-US";

        /// <summary>
        /// Gets or sets the grid page size.
        /// </summary>
        [Required]
        public int GridPageSize { get; set; } = 10;
    }
}
