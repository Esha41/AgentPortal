using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Intelli.AgentPortal.Api.DTO
{
    /// <summary>
    /// The configurations dto.
    /// </summary>
    public class ConfigurationDto
    {
        /// <summary>
        /// Gets or sets a value indicating whether password require non alphanumeric.
        /// </summary>
        [Required]
        public bool PasswordRequireNonAlphanumeric { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether password require lowercase.
        /// </summary>
        [Required]
        public bool PasswordRequireLowercase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether password require uppercase.
        /// </summary>
        [Required]
        public bool PasswordRequireUppercase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether password require digit.
        /// </summary>
        [Required]
        public bool PasswordRequireDigit { get; set; }

        /// <summary>
        /// Gets or sets the password's required length.
        /// </summary>
        [Required]
        public int PasswordRequiredLength { get; set; }

        /// <summary>
        /// Gets or sets the value to restrict last used passwords.
        /// e.g., 3 means user cannot use its last 3 passwords.
        /// </summary>
        [Required]
        public int RestrictLastUsedPasswords { get; set; }

        /// <summary>
        /// Gets or sets the force password change days.
        /// User must change password after specified days.
        /// </summary>
        [Required]
        public int ForcePasswordChangeDays { get; set; }

        /// <summary>
        /// Converts the configurations to password options.
        /// </summary>
        /// <returns>A PasswordOptions.</returns>
        internal PasswordOptions ToPasswordOptions()
        {
            return new PasswordOptions
            {
                RequireDigit = PasswordRequireDigit,
                RequiredLength = PasswordRequiredLength,
                RequireLowercase = PasswordRequireLowercase,
                RequireNonAlphanumeric = PasswordRequireNonAlphanumeric,
                RequireUppercase = PasswordRequireUppercase,
                RequiredUniqueChars = 0
            };
        }
    }
}
