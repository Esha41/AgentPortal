namespace Intelli.AgentPortal.Api.Helpers
{
    /// <summary>
    /// The auth constants.
    /// </summary>
    public interface IAuthConstants
    {
        /// <summary>
        /// The jwt issuer.
        /// </summary>
        public const string JwtIssuer = "Jwt:Issuer";

        /// <summary>
        /// The jwt audience.
        /// </summary>
        public const string JwtAudience = "Jwt:Audience";

        /// <summary>
        /// The jwt secret key.
        /// </summary>
        public const string JwtSecretKey = "Jwt:SecretKey";

        /// <summary>
        /// The token expiry in hours.
        /// </summary>
        public const string TokenExpiryInHours = "Jwt:TokenExpiryInHours";

        /// <summary>
        /// The user name.
        /// </summary>
        public const string UserName = "UserName";
    }
}
