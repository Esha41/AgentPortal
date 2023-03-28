using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwTransactionsSignedButNotCompleted
    {
        public string CompanyName { get; set; }
        public string RequestId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
