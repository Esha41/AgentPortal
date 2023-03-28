using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchItem
    {
        public BatchItem()
        {
            BatchItemFields = new HashSet<BatchItemField>();
            BatchItemPages = new HashSet<BatchItemPage>();
            InverseParent = new HashSet<BatchItem>();
        }

        public int Id { get; set; }
        public int BatchId { get; set; }
        public int BatchItemStatusId { get; set; }
        public int? DocumentClassId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ParentId { get; set; }
        public int? AspNetUserId { get; set; }
        public bool? IsValid { get; set; }
        public bool? IncludeInOnboarding { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual BatchItemStatus BatchItemStatus { get; set; }
        public virtual DocumentClasses DocumentClass { get; set; }
        public virtual BatchItem Parent { get; set; }
        public virtual ICollection<BatchItemField> BatchItemFields { get; set; }
        public virtual ICollection<BatchItemPage> BatchItemPages { get; set; }
        public virtual ICollection<BatchItem> InverseParent { get; set; }
    }
}
