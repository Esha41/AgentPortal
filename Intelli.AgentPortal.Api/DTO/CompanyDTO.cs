using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intelli.AgentPortal.Api.DTO
{
    /// <summary>
    /// The company DTO.
    /// </summary>
    public class CompanyDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the call back url.
        /// </summary>
        [MaxLength(250)]
        public string CallBackUrl { get; set; }

        /// <summary>
        /// Gets or sets the hawk app id.
        /// </summary>
        [MaxLength(50)]
        public string HawkAppId { get; set; }

        /// <summary>
        /// Gets or sets the hawk user.
        /// </summary>
        [MaxLength(50)]
        public string HawkUser { get; set; }

        /// <summary>
        /// Gets or sets the hawk secret.
        /// </summary>
        [MaxLength(50)]
        public string HawkSecret { get; set; }

        /// <summary>
        /// Gets or sets the ftp host name.
        /// </summary>
        [MaxLength(50)]
        public string FtpHostName { get; set; }

        /// <summary>
        /// Gets or sets the ftp user name.
        /// </summary>
        [MaxLength(50)]
        public string FtpUserName { get; set; }

        /// <summary>
        /// Gets or sets the ftp password.
        /// </summary>
        [MaxLength(50)]
        public string FtpPassword { get; set; }

        /// <summary>
        /// Gets or sets the ftp directory.
        /// </summary>
        [MaxLength(150)]
        public string FtpDirectory { get; set; }

        /// <summary>
        /// Gets or sets the retries when fail published.
        /// </summary>
        [Required]
        public int RetriesWhenFailPublished { get; set; }

        /// <summary>
        /// Gets or sets the gdprdays to be kept.
        /// </summary>
        [Required]
        public int GdprdaysToBeKept { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [MaxLength(10)]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the ftp port.
        /// </summary>
        public int? FtpPort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ftp user should use secure protocol.
        /// </summary>
        public bool? FtpUserSecureProtocol { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ftp is active.
        /// </summary>
        public bool? FtpActive { get; set; }

        /// <summary>
        /// Gets or sets the ftp response host name.
        /// </summary>
        [MaxLength(50)]
        public string FtpResponseHostName { get; set; }

        /// <summary>
        /// Gets or sets the ftp response user name.
        /// </summary>
        [MaxLength(50)]
        public string FtpResponseUserName { get; set; }

        /// <summary>
        /// Gets or sets the ftp response password.
        /// </summary>
        [MaxLength(50)]
        public string FtpResponsePassword { get; set; }

        /// <summary>
        /// Gets or sets the ftp response port.
        /// </summary>
        public int? FtpResponsePort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ftp response user uses secure protocol.
        /// </summary>
        public bool? FtpResponseUserSecureProtocol { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ftp response is active.
        /// </summary>
        public bool? FtpResponseActive { get; set; }

        /// <summary>
        /// Gets or sets the ftp response directory.
        /// </summary>
        [MaxLength(150)]
        public string FtpResponseDirectory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether response should be with ftp.
        /// </summary>
        public bool? ResponseWithFtp { get; set; }

        /// <summary>
        /// Gets or sets the similarity threshold.
        /// </summary>
        public int? SimilarityThreshold { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether enabled.
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// Gets or sets the slaminutes.
        /// </summary>
        public int? Slaminutes { get; set; }

        /// <summary>
        /// Gets or sets the slabatch quantity.
        /// </summary>
        public int? SlabatchQuantity { get; set; }

        /// <summary>
        /// Gets or sets the slaimportance.
        /// </summary>
        [Required]
        public int Slaimportance { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [MaxLength(150)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether signed is company.
        /// </summary>
        [Required]
        public bool IsSignedCompany { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether send rejection reason as code.
        /// </summary>
        [Required]
        public bool SendRejectionReasonAsCode { get; set; }

        /// <summary>
        /// Gets or sets the logo.
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether send link.
        /// </summary>
        [Required]
        public bool SendLink { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether supports calls.
        /// </summary>
        [Required]
        public bool SupportsCalls { get; set; }

        /// <summary>
        /// Gets or sets the max call time.
        /// </summary>
        public int? MaxCallTime { get; set; }

        /// <summary>
        /// Gets or sets the video call back url.
        /// </summary>
        [MaxLength(250)]
        public string VideoCallBackUrl { get; set; }

        /// <summary>
        /// Gets or sets the agent controller.
        /// </summary>
        [MaxLength(100)]
        public string AgentController { get; set; }

        /// <summary>
        /// Gets or sets the customer retries.
        /// </summary>
        public int? CustomerRetries { get; set; }

        /// <summary>
        /// Gets or sets the smsprovider.
        /// </summary>
        public string Smsprovider { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        public long CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        public long UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the documents per companies.
        /// </summary>
        public virtual IEnumerable<DocumentsPerCompanyDTO> DocumentsPerCompany { get; set; }

        /// <summary>
        /// Gets or sets the system users.
        /// </summary>
        public virtual IEnumerable<UserReadDTO> SystemUsers { get; set; }

        /// <summary>
        /// Gets or sets the users per company.
        /// </summary>
        public int UsersPerCompany { get; set; }
 /*       public string SMTPPort { get; set; }
        public string SMTPHost { get; set; }
        public string SMTPFrom { get; set; }
        public string SMTPFromUsername { get; set; }
        public string SMTPFromPassword { get; set; }
        public string SMTPSubject { get; set; }
        public string BasicAuthUserName { get; set; }
        public string BasicAuthPassword { get; set; }
        public string BearerAuthURL { get; set; }
        public string BearerUserName { get; set; }
        public string BearerPassword { get; set; }
        public string GrandType { get; set; }
        public string PriorityQueueNotification { get; set; }
        public string CloudStorage { get; set; }
        public string SoapCallback { get; set; }
        public string CallBackConfiguration { get; set; }
        public string AgentPortalLogo { get; set; }
        public string VideoResponseCallbackOptions { get; set; }
        public string JsonResponseCallbackOptions { get; set; }
        public string DocumentsDownloadAccessEndpoint { get; set; }
        public string DocumentsDownloadAccessFetchUrl { get; set; }
        public int ExpirationDaysToBeKept { get; set; }
        public bool SendResponseUpponGDPR { get; set; }
        public bool HasOwnValidation { get; set; } = false;
        public bool AutoRedirect { get; set; } = false;
        public bool SupportsInstantRecognition { get; set; } = false;
        public bool FinalResponseAwaitsVideo { get; set; }*/
    }
}