using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwFriendlyBatchSourceAppPage
    {
        public int Id { get; set; }
        public string BatchSource { get; set; }
        public string CurrentAppPage { get; set; }
        public string NextAppPage { get; set; }
        public string BackAppPage { get; set; }
        public int Ordering { get; set; }
        public int? DocumentSignConfigId { get; set; }
        public bool? RequiredSign { get; set; }
    }
}
