using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchHistory
    {
        public BatchHistory()
        {
            BatchHistoryJsonResults = new HashSet<BatchHistoryJsonResult>();
            BatchHistoryMeta = new HashSet<BatchHistoryMetum>();
        }

        public int Id { get; set; }
        public int BatchId { get; set; }
        public string SynchordiaRequestId { get; set; }
        public DateTime? RegisterDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public bool IsLast { get; set; }
        public bool? VerificationStatus { get; set; }
        public string VerificationRejectionReason { get; set; }
        public DateTime? StartProcessDate { get; set; }
        public bool? AgentVerificationStatus { get; set; }
        public string Agent { get; set; }
        public DateTime? AgentVerificationStatusDate { get; set; }
        public string IsAlive { get; set; }
        public string FaceMatching { get; set; }
        public int? BatchHistoryStatusId { get; set; }
        public int? RetriesCount { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool? HistoryHubNotified { get; set; }
        public virtual Batch Batch { get; set; }
        public virtual ICollection<BatchHistoryJsonResult> BatchHistoryJsonResults { get; set; }
        public virtual ICollection<BatchHistoryMetum> BatchHistoryMeta { get; set; }
        public virtual ICollection<BatchHistoryItem> BatchHistoryItems { get; set; }
    }
}
