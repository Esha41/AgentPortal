using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchHistoryItem
    {
        public BatchHistoryItem()
        {
            BatchHistoryItemFields = new HashSet<BatchHistoryItemField>();
            BatchHistoryItemPages = new HashSet<BatchHistoryItemPage>();
        }

        public int Id { get; set; }
        public int? DocumentClassId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsValid { get; set; }
        public int BatchHistoryId { get; set; }
        public int? BatchSourceUploadDocId { get; set; }
        public bool? IncludeInOnboarding { get; set; }

        public virtual ICollection<BatchHistoryItemField> BatchHistoryItemFields { get; set; }
        public virtual ICollection<BatchHistoryItemPage> BatchHistoryItemPages { get; set; }
    }
}
