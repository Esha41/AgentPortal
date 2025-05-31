namespace Intelli.AgentPortal.Api.Services.Cache.Impl
{
    /// <summary>
    /// The cache settings.
    /// </summary>
    public class CacheSettings
    {
        /// <summary>
        /// Gets the size limit.
        /// </summary>
        public int? SizeLimit { get; set; }

        /// <summary>
        /// Gets the sliding expiration in minutes.
        /// </summary>
        public int SlidingExpirationInMinutes { get; set; }

        /// <summary>
        /// Gets the absolute expiration in minutes.
        /// </summary>
        public int AbsoluteExpirationInMinutes { get; set; }
    }
}