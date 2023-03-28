using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchItemField
    {
        public int Id { get; set; }
        public int BatchItemId { get; set; }
        public int DocumentClassFieldId { get; set; }
        public int? DictionaryValueId { get; set; }
        public int? CreatedByAspNetUserId { get; set; }
        public bool? IsLast { get; set; }
        public string RegisteredFieldValue { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? DictionaryValueIdOld { get; set; }
        public string RegisteredFieldValueOld { get; set; }
        public bool? IsUpdated { get; set; }

        public virtual BatchItem BatchItem { get; set; }
        public virtual AspNetUser CreatedByAspNetUser { get; set; }
        public virtual BopDictionary DictionaryValue { get; set; }
        public virtual DocumentClassField DocumentClassField { get; set; }
    }
}
