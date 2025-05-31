using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchHistoryItemField
    {
        public int Id { get; set; }
        public int BatchHistoryItemId { get; set; }
        public int DocumentClassFieldId { get; set; }
        public string RegisteredFieldValue { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual BatchHistoryItem BatchHistoryItem { get; set; }
        public virtual DocumentClassField DocumentClassField { get; set; }
    }
}
