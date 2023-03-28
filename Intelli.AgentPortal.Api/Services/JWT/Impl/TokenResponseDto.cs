namespace Intelli.AgentPortal.Api.Services.JWT.Impl
{
    /// <summary>
    /// The generated token response dto.
    /// </summary>
    public class TokenResponseDto
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the jti.
        /// The unique identifier of jwt. (A Guid)
        /// </summary>
        public string Jti { get; set; }
    }
}
