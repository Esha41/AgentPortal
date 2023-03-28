using System;
using System.Collections.Generic;

namespace Intelli.AgentPortal.Api.DTO
{
    public class FullBatchDto
    {
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
        public virtual BatchStatusDTO BatchStatus { get; set; }
        public virtual BatchCompaniesDTO Company { get; set; }
        public virtual ICollection<BatchMetumDTO> BatchMeta { get; set; }
    }
}
