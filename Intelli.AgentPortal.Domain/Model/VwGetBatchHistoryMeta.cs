using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwGetBatchHistoryMeta
    {
        public int Id { get; set; }
        public string FieldValue { get; set; }
        public string Uilabel { get; set; }
        public int BatchHistoryMetaId { get; set; }
    }
}
