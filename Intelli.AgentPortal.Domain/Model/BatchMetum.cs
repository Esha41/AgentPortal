using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchMetum
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public int DocumentClassFieldId { get; set; }
        public int? DictionaryValueId { get; set; }
        public string FieldValue { get; set; }
        public virtual Batch Batch { get; set; }
    }
}
