using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwGetBatchItemField
    {
        public int BatchItemId { get; set; }
        public int BatchItemFieldId { get; set; }
        public string RegisteredFieldValue { get; set; }
        public string Uilabel { get; set; }
        public string MappedName { get; set; }
    }
}
