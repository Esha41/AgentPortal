using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class ServiceLastExcecution
    {
        public string ServiceName { get; set; }
        public DateTime Time { get; set; }
    }
}
