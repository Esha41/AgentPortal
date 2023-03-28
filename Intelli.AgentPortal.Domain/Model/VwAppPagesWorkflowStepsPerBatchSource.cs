using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwAppPagesWorkflowStepsPerBatchSource
    {
        public int BatchSourceId { get; set; }
        public string CurrentAppPage { get; set; }
        public int Ordering { get; set; }
        public string Steps { get; set; }
    }
}
