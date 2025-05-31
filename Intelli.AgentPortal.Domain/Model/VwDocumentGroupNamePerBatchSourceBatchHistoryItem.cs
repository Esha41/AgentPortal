using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwDocumentGroupNamePerBatchSourceBatchHistoryItem
    {
        public int Id { get; set; }
        public string BatchSourceCode { get; set; }
        public string DocumentGroupName { get; set; }
        public bool Mandatory { get; set; }
        public int Expr1 { get; set; }
        public int DocumentGroupNameId { get; set; }
        public string RequestId { get; set; }
        public int? DocumentClassId { get; set; }
        public bool? IsValid { get; set; }
        public string DocumentClassIds { get; set; }
    }
}
