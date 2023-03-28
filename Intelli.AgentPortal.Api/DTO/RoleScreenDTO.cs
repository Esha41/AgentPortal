using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intelli.AgentPortal.Api.DTO
{
    /// <summary>
    /// The role screen DTO.
    /// </summary>
    public class RoleScreenDTO
    {
        /// <summary>
        /// Gets or sets the Screen Name.
        /// </summary>
        public string ScreenName { get; set; }

        /// <summary>
        /// Gets or sets the Screen Priviliges.
        /// 0 = None, 1 = Custom, 2 = Full
        /// </summary>
        [Required]
        public int ScreenPriviliges { get; set; }

        /// <summary>
        /// Gets or sets the Role Id.
        /// </summary>
        [Required]
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the Screen Id.
        /// </summary>
        [Required]
        public int ScreenId { get; set; }

        /// <summary>
        /// Gets or sets the Screen Element Priviliges.
        /// </summary>
        public List<RoleScreenElementDTO> ScreenElementPriviliges { get; set; }

        /// <summary>
        /// Have no privilege.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool HasNoPrivilege()
        {
            return ScreenPriviliges == Domain.Model.RoleScreen.NO_PRIVILEGE;
        }

        /// <summary>
        /// Have admin privilege.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool HasAdminPrivilege()
        {
            return ScreenPriviliges == Domain.Model.RoleScreen.FULL_CONTROL;
        }

        /// <summary>
        /// Have custom privilege.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool HasCustomPrivilege()
        {
            return ScreenPriviliges == Domain.Model.RoleScreen.CUSTOM_PRIVILEGE;
        }

        /// <summary>
        /// Have admin or custom privilege.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool HasAdminOrCustomPrivilege()
        {
            return ScreenPriviliges == Domain.Model.RoleScreen.FULL_CONTROL ||
                    ScreenPriviliges == Domain.Model.RoleScreen.CUSTOM_PRIVILEGE;
        }
    }
}