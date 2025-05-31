using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class Company
    {
        public Company()
        {
            DocumentsPerCompany = new HashSet<DocumentsPerCompany>();
            AspNetResellers = new HashSet<AspNetReseller>();
          //  BatchSources = new HashSet<BatchSource>();
          //  Batches = new HashSet<Batch>();
          //  CountriesPerCompanies = new HashSet<CountriesPerCompany>();
            SystemRoles = new HashSet<SystemRole>();
            UserCompanies = new HashSet<UserCompany>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CallBackUrl { get; set; }
        public string HawkAppId { get; set; }
        public string HawkUser { get; set; }
        public string HawkSecret { get; set; }
        public string FtpHostName { get; set; }
        public string FtpUserName { get; set; }
        public string FtpPassword { get; set; }
        public string FtpDirectory { get; set; }
        public int RetriesWhenFailPublished { get; set; }
        public int GdprdaysToBeKept { get; set; }
        public string Code { get; set; }
        public int? FtpPort { get; set; }
        public bool? FtpUserSecureProtocol { get; set; }
        public bool? FtpActive { get; set; }
        public string FtpResponseHostName { get; set; }
        public string FtpResponseUserName { get; set; }
        public string FtpResponsePassword { get; set; }
        public int? FtpResponsePort { get; set; }
        public bool? FtpResponseUserSecureProtocol { get; set; }
        public bool? FtpResponseActive { get; set; }
        public string FtpResponseDirectory { get; set; }
        public bool? ResponseWithFtp { get; set; }
        public int? SimilarityThreshold { get; set; }
        public bool? Enabled { get; set; }
        public int? Slaminutes { get; set; }
        public int? SlabatchQuantity { get; set; }
        public int Slaimportance { get; set; }
        public string Email { get; set; }
        public bool IsSignedCompany { get; set; }
        public int? Smtpport { get; set; }
        public string Smtphost { get; set; }
        public string Smtpfrom { get; set; }
        public string SmtpfromUsername { get; set; }
        public string SmtpfromPassword { get; set; }
        public string Smtpsubject { get; set; }
        public string BasicAuthUserName { get; set; }
        public string BasicAuthPassword { get; set; }
        public bool? SendRejectionReasonAsCode { get; set; }
        public string BearerAuthUrl { get; set; }
        public string BearerUserName { get; set; }
        public string BearerPassword { get; set; }
        public string GrandType { get; set; }
        public string Logo { get; set; }
        public bool SendLink { get; set; }
        public bool SupportsCalls { get; set; }
        public int? MaxCallTime { get; set; }
        public string VideoCallBackUrl { get; set; }
        public string AgentController { get; set; }
        public int? CustomerRetries { get; set; }
        public string Smsprovider { get; set; }
        public short? ExpirationDaysToBeKept { get; set; }
        public bool? SendResponseUpponGdpr { get; set; }
        public string PriorityQueueNotification { get; set; }
        public string CloudStorage { get; set; }
        public bool HasOwnValidation { get; set; }
        public bool? FinalResponseAwaitsVideo { get; set; }
        public string SoapCallback { get; set; }
        public string CallBackConfiguration { get; set; }
        public bool AutoRedirect { get; set; }
        public string AgentPortalLogo { get; set; }
        public bool SupportsInstantRecognition { get; set; }
        public string VideoResponseCallbackOptions { get; set; }
        public string JsonResponseCallbackOptions { get; set; }
        public string DocumentsDownloadAccessEndpoint { get; set; }
        public string DocumentsDownloadAccessFetchUrl { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public string UsersPerCompany { get; set; }

        public virtual CompaniesMonitoringCofiguration CompaniesMonitoringCofiguration { get; set; }

        public virtual ICollection<AspNetReseller> AspNetResellers { get; set; }
        public virtual ICollection<BatchSource> BatchSources { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<CountriesPerCompany> CountriesPerCompanies { get; set; }
        public virtual ICollection<SystemRole> SystemRoles { get; set; }
        public virtual ICollection<DocumentsPerCompany> DocumentsPerCompany { get; set; }
        public virtual ICollection<UserCompany> UserCompanies { get; set; }

    }
}
