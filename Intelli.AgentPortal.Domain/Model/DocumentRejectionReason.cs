using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class DocumentRejectionReason
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Descr { get; set; }
    }
}
