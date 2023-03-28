using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwReportingBatchSourceAppPagesProgress
    {
        public int Id { get; set; }
        public int BatchSourceId { get; set; }
        public int CurrentAppPageId { get; set; }
        public int? NextAppPageId { get; set; }
        public int Ordering { get; set; }
        public int? DocumentSignConfigId { get; set; }
        public bool? RequiredSign { get; set; }
        public int? BackAppPageId { get; set; }
        public bool? CanReLoad { get; set; }
        public string ControllerPathAction { get; set; }
        public string FriendlyName { get; set; }
        public int? CompanyId { get; set; }
        public double? Progress { get; set; }
    }
}
