using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchHistoryJsonResult
    {
        public int Id { get; set; }
        public int BatchHistoryId { get; set; }
        public string JsonResult { get; set; }

        public virtual BatchHistory BatchHistory { get; set; }
    }
}
