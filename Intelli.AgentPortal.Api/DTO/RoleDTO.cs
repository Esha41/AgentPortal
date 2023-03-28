using Intelli.AgentPortal.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intelli.AgentPortal.Api.DTO
{
    /// <summary>
    /// The role DTO.
    /// </summary>
    public class RoleDTO
    {
        /// <summary>
        /// Gets or sets the role id.
        /// </summary>

        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the RoleDescription.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool? IsActive { get; set; }

        public int? CompanyId { get; set; }

        public int? Priority { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public List<UserReadDTO> Users { get; set; }

        /// <summary>
        /// Gets or sets the screens.
        /// </summary>
        public List<RoleScreenDTO> Screens { get; set; }

        public CompanyDTO Company { get; set; }

        public List<SystemRole> companyRoles { get; set; }
    }
}
