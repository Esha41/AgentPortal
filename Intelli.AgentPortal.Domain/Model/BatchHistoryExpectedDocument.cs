using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchHistoryExpectedDocument
    {
        public int Id { get; set; }
        public int BatchHistoryId { get; set; }
        public string PoI { get; set; }
        public string PoA { get; set; }
        public string PoT { get; set; }
        public string PoO { get; set; }
        public string PoP { get; set; }

        public virtual BatchHistory BatchHistory { get; set; }
    }
}
