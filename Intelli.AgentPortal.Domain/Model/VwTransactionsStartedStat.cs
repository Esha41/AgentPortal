using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwTransactionsStartedStat
    {
        public int BatchId { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BatchStatus { get; set; }
        public DateTime? GetInVideoQueueDate { get; set; }
        public DateTime? StartVideoCallDate { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}
