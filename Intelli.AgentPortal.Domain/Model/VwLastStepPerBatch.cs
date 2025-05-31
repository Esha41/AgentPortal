using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwLastStepPerBatch
    {
        public int BatchId { get; set; }
        public string RequestId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public bool Visited { get; set; }
        public bool Signed { get; set; }
        public bool Completed { get; set; }
    }
}
