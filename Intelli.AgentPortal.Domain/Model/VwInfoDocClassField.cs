using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwInfoDocClassField
    {
        public int BatchSourceId { get; set; }
        public int? Id { get; set; }
        public string EnumValue { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsMandatory { get; set; }
        public int? Ordering { get; set; }
        public int? FieldValue { get; set; }
    }
}
