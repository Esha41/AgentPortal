using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class Batch
    {
        public Batch()
        {
            BatchAppPagesHistories = new HashSet<BatchAppPagesHistory>();
            BatchContractSignConfigs = new HashSet<BatchContractSignConfig>();
            BatchHistories = new HashSet<BatchHistory>();
            BatchItems = new HashSet<BatchItem>();
            BatchMeta = new HashSet<BatchMetum>();
            BatchmetaToDeletes = new HashSet<BatchmetaToDelete>();
        }

        public int Id { get; set; }
        public string RequestId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BatchStatusId { get; set; }
        public int BatchSourceId { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int? RetriesCount { get; set; }
        public string CurrentOtp { get; set; }
        public DateTime? OtpvalidUntil { get; set; }
        public bool? AppliedGdpr { get; set; }
        public DateTime? StartProcessDate { get; set; }
        public string Token { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public int Otpcounter { get; set; }
        public int CompanyId { get; set; }
        public int? LockedBy { get; set; }
        public int VideoPublishRetriesCount { get; set; }
        public int VideoStatus { get; set; }
        public DateTime? GetInVideoQueueDate { get; set; }
        public DateTime? StartVideoCallDate { get; set; }
        public DateTime? EndVideoCallDate { get; set; }
        public DateTime? DisconnectionTime { get; set; }
        public int? DisconnectionsCount { get; set; }
        public Int16? UpponGdprpublishRetriesCount { get; set; }
        public bool? UpponGdprpublishStatus { get; set; }
        public bool? VerifiedHubNotified { get; set; }
        public DateTime? RepublishEndDate { get; set; }
        public DateTime? LastRepublishDate { get; set; }
        public int? RepublishTriesCount { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public virtual BatchSource BatchSource { get; set; }
        public virtual BatchStatus BatchStatus { get; set; }
        public virtual Company Company { get; set; }
        public virtual BatchVideoStatus VideoStatusNavigation { get; set; }
        public virtual ICollection<BatchAppPagesHistory> BatchAppPagesHistories { get; set; }
        public virtual ICollection<BatchContractSignConfig> BatchContractSignConfigs { get; set; }
        public virtual ICollection<BatchHistory> BatchHistories { get; set; }
        public virtual ICollection<BatchItem> BatchItems { get; set; }
        public virtual ICollection<BatchMetum> BatchMeta { get; set; }
        public virtual ICollection<BatchmetaToDelete> BatchmetaToDeletes { get; set; }
    }
}
