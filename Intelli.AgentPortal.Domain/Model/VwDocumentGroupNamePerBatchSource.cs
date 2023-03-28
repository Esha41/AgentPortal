using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwDocumentGroupNamePerBatchSource
    {
        public int BatchSourceId { get; set; }
        public string BatchSourceCode { get; set; }
        public string DocumentGroupName { get; set; }
        public bool Mandatory { get; set; }
        public int BatchSourceUploadDocId { get; set; }
    }
}
