using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchHistoryItemPage
    {
        public int Id { get; set; }
        public int BatchHistoryItemId { get; set; }
        public string FileName { get; set; }
        public int Number { get; set; }
        public string OriginalFileName { get; set; }
        public bool? IsLast { get; set; }

        public virtual BatchHistoryItem BatchHistoryItem { get; set; }
    }
}
