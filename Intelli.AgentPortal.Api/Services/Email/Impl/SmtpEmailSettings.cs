
namespace Intelli.AgentPortal.Api.Services.Email.Impl
{
    public class SmtpEmailSettings
    {
        /// <summary>
        /// Gets or sets the send grid user.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the sender email.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the send grid key.
        /// </summary>
        public string FromEmailUsername { get; set; }

        /// <summary>
        /// Gets or sets the send grid key.
        /// </summary>
        public string FromEmailPassword { get; set; }

        /// <summary>
        /// Gets or sets the send grid key.
        /// </summary>
        public string From { get; set; }

        public bool EnableSsl { get; set; }

        public int TimeOut { get; set; }

        public bool UseDefaultCredentials { get; set; }

        public string DisplayName { get; set; }
    }
}
