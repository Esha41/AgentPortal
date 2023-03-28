using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwRetriesForPendingTransaction
    {
        public int? Retries { get; set; }
        public int BatchId { get; set; }
        public string RequestId { get; set; }
        public string CompanyName { get; set; }
        public string CreatedDate { get; set; }
    }
}
