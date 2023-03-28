using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class AdditionalDocumentsOrdering
    {
        public int Id { get; set; }
        public string DocumentTemplateName { get; set; }
        public int Ordering { get; set; }
    }
}
