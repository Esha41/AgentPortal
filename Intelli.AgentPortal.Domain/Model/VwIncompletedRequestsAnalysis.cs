using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwIncompletedRequestsAnalysis
    {
        public int BatchId { get; set; }
        public bool Visited { get; set; }
        public bool Signed { get; set; }
        public bool Completed { get; set; }
        public string CreatedDate { get; set; }
        public string CurrentPage { get; set; }
        public string NextPage { get; set; }
        public string Company { get; set; }
    }
}
