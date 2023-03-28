using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwGetBatchesForBatchHistoryHubNotification
    {
        public int BatchId { get; set; }
        public string RequestId { get; set; }
        public int BatchHistoryId { get; set; }
        public bool? HistoryHubNotified { get; set; }
    }
}
