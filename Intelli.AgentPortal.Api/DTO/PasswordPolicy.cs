using System.Text;

namespace Intelli.AgentPortal.Api.DTO
{
    /// <summary>
    /// The password policy.
    /// </summary>
    public class PasswordPolicy
    {
        private readonly ConfigurationDto _dto;

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordPolicy"/> class.
        /// </summary>
        /// <param name="dto">The ConfigurationDto.</param>
        public PasswordPolicy(ConfigurationDto dto)
        {
            _dto = dto;
        }

        /// <summary>
        /// Gets or sets the minimum length a password must be. Defaults to 6.
        /// </summary>
        public int RequiredLength
        {
            get
            {
                return _dto.PasswordRequiredLength;
            }
        }

        /// <summary>
        ///     Gets or sets a flag indicating if passwords must contain a non-alphanumeric character.
        ///     Defaults to true.
        ///
        /// Value:
        ///     True if passwords must contain a non-alphanumeric character, otherwise false.
        /// </summary>
        public bool RequireNonAlphanumeric
        {
            get
            {
                return _dto.PasswordRequireNonAlphanumeric;
            }
        }

        /// <summary>
        ///     Gets or sets a flag indicating if passwords must contain a lower case ASCII character.
        ///     Defaults to true.
        ///
        /// Value:
        ///     True if passwords must contain a lower case ASCII character.
        /// </summary>
        public bool RequireLowercase
        {
            get
            {
                return _dto.PasswordRequireLowercase;
            }
        }

        /// <summary>
        ///     Gets or sets a flag indicating if passwords must contain a upper case ASCII character.
        ///     Defaults to true.
        ///
        /// Value:
        ///     True if passwords must contain a upper case ASCII character.
        /// </summary>
        public bool RequireUppercase
        {
            get
            {
                return _dto.PasswordRequireUppercase;
            }
        }

        /// <summary>
        ///     Gets or sets a flag indicating if passwords must contain a digit. Defaults to
        ///     true.
        ///
        /// Value:
        ///     True if passwords must contain a digit.
        /// </summary>
        public bool RequireDigit
        {
            get
            {
                return _dto.PasswordRequireDigit;
            }
        }

        /// <summary>
        /// Gets the regular expression.
        /// </summary>
        public string RegularExpression
        {
            get
            {
                var sb = new StringBuilder();

                // Start
                sb.Append("^");

                // RequireNonAlphanumeric
                if (RequireNonAlphanumeric) sb.Append("(?=.*[^a-zA-Z0-9])");

                // RequireLowercase
                if (RequireLowercase) sb.Append("(?=.*[a-z])");

                // RequireUppercase
                if (RequireUppercase) sb.Append("(?=.*[A-Z])");

                // RequireDigit
                if (RequireDigit) sb.Append("(?=.*[0-9])");

                // RequiredLength
                sb.Append(".{" + RequiredLength + ",}");

                // End
                sb.Append("");

                return sb.ToString();
            }
        }
    }
}
