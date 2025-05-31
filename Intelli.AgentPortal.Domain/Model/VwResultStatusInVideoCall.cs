using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwResultStatusInVideoCall
    {
        public string ReasonStopped { get; set; }
        public int? CountForReasonStopped { get; set; }
        public string CompanyName { get; set; }
        public string InVideoQueueDate { get; set; }
    }
}
