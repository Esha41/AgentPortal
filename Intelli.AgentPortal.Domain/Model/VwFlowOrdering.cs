using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwFlowOrdering
    {
        public int Ordering { get; set; }
        public int BatchSourceId { get; set; }
        public string BatchSource { get; set; }
        public string Description { get; set; }
        public string ControllerPathAction { get; set; }
        public string FriendlyName { get; set; }
    }
}
