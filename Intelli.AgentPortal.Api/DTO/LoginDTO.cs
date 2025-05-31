using System.ComponentModel.DataAnnotations;

namespace Intelli.AgentPortal.Api.DTO
{
    /// <summary>
    /// To encapsulate UserName and Password fields
    /// </summary>
    public class LoginDTO
    {
        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
