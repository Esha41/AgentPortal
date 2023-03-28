using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwAppPagesWorkflowPerBatchSource
    {
        public int BatchSourceId { get; set; }
        public string CurrentAppPage { get; set; }
        public string NextAppPage { get; set; }
        public string PreviousAppPage { get; set; }
        public string DocumentTemplateName { get; set; }
        public string SignatureConfiguration { get; set; }
        public bool? RequiredSign { get; set; }
        public int CurrentAppPageId { get; set; }
        public int BatchSourceAppPageId { get; set; }
        public int Ordering { get; set; }
        public bool? CanReLoad { get; set; }
    }
}
