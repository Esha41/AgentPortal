using Intelli.AgentPortal.Domain.Model.Core;
using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class DboVwDocumentGroupNamesForBatchHistory
    {
        public int Id { get; set; }
        public int BatchHistoryId { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string DocumentGroupName { get; set; }
        public bool? IsValid { get; set; }
        public string FileName { get; set; }
        public int? PageId { get; set; }
        public int? DocumentGroupNameId { get; set; }
        public bool? IsLast { get; set; }
    }
}
