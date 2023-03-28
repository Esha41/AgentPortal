using System.Collections.Generic;

namespace Intelli.AgentPortal.Api.DTO
{
    /// <summary>
    /// The user read privileges DTO.
    /// </summary>
    public class UserReadPrivilegesDTO : UserReadDTO
    {
        /// <summary>
        /// Gets or sets the screen priviliges.
        /// </summary>
        public List<RoleScreenDTO> ScreenPriviliges { get; set; }

        /// <summary>
        /// Gets or sets the user preferences.
        /// </summary>
        public UserPreferencesDTO Preferences { get; set; }

        /// <summary>
        /// Gets or sets the password policy.
        /// </summary>
        public PasswordPolicy PasswordPolicy { get; set; }
    }
}
