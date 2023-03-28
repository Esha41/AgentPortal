using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class InfoDocClassField
    {
        public int Id { get; set; }
        public int DocumentClassFieldId { get; set; }
        public int BatchSourceId { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsMandatory { get; set; }
        public int? Ordering { get; set; }

        public virtual BatchSource BatchSource { get; set; }
        public virtual DocumentClassField DocumentClassField { get; set; }
    }
}
