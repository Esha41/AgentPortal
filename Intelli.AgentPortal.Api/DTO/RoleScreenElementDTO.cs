using System.ComponentModel.DataAnnotations;

namespace Intelli.AgentPortal.Api.DTO
{
    /// <summary>
    /// The role screen element DTO.
    /// </summary>
    public class RoleScreenElementDTO
    {
        /// <summary>
        /// Gets or sets the Element Name.
        /// </summary>
        public string ElementName { get; set; }

        /// <summary>
        /// Gets or sets the Priviliges.
        /// 0 = None, 1 = Read only, 2 = Full control
        /// </summary>
        [Required]
        public int Priviliges { get; set; }

        /// <summary>
        /// Gets or sets the Role Id.
        /// </summary>
        [Required]
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the Screen Element Id.
        /// </summary>
        [Required]
        public int ScreenElementId { get; set; }
    }
}