using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwStatsForTransactionsInVideoCallQueue
    {
        public int BatchId { get; set; }
        public string RequestId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string WaitingTime { get; set; }
        public string VideoCallTime { get; set; }
        public string TransactionTime { get; set; }
        public DateTime? DisconnectionTime { get; set; }
        public int? DisconnectionsCount { get; set; }
        public string BatchStatusValue { get; set; }
        public string BatchVideoStatusValue { get; set; }
        public string Agent { get; set; }
        public string VideoSessionResult { get; set; }
    }
}
