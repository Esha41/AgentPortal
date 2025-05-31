using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchHistoryMetum
    {
        public int Id { get; set; }
        public int BatchHistoryId { get; set; }
        public int DocumentClassFieldId { get; set; }
        public int? DictionaryValueId { get; set; }
        public string FieldValue { get; set; }

        public virtual BatchHistory BatchHistory { get; set; }
        public virtual DocumentClassField DocumentClassField { get; set; }
    }
}
