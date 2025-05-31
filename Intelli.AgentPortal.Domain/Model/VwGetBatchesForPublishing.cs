using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwGetBatchesForPublishing
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
        public int IsMidPublishing { get; set; }
        public int? BatchHistoryId { get; set; }
    }
}
