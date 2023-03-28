using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchAppPagesHistory
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public int BatchSourceAppPageId { get; set; }
        public bool Visited { get; set; }
        public bool Signed { get; set; }
        public bool Completed { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Retries { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual BatchSourceAppPage BatchSourceAppPage { get; set; }
    }
}
