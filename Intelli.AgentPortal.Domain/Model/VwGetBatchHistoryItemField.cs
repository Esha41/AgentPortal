using Intelli.AgentPortal.Domain.Model.Core;
using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwGetBatchHistoryItemField
    {
        public int Id { get; set; }
        public int ItemFieldId { get; set; }
        public string RegisteredFieldValue { get; set; }
        public string Uilabel { get; set; }
        public string MappedName { get; set; }
    }
}
