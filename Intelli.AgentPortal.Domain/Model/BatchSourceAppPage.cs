using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchSourceAppPage
    {
        public BatchSourceAppPage()
        {
            BatchAppPagesHistories = new HashSet<BatchAppPagesHistory>();
        }

        public int Id { get; set; }
        public int BatchSourceId { get; set; }
        public int CurrentAppPageId { get; set; }
        public int? NextAppPageId { get; set; }
        public int Ordering { get; set; }
        public int? DocumentSignConfigId { get; set; }
        public bool? RequiredSign { get; set; }
        public int? BackAppPageId { get; set; }
        public bool? CanReLoad { get; set; }

        public virtual BatchSource BatchSource { get; set; }
        public virtual AppPage CurrentAppPage { get; set; }
        public virtual DocumentSignConfig DocumentSignConfig { get; set; }
        public virtual ICollection<BatchAppPagesHistory> BatchAppPagesHistories { get; set; }
    }
}
