using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchVideoPriority
    {
        public int BatchId { get; set; }
        public int Priotity { get; set; }
        public int EstimatedTimeInMin { get; set; }
        public int? CompanyId { get; set; }
        public int? MaxCallTime { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? BatchStatusId { get; set; }
        public string RequestId { get; set; }
        public int? LockedBy { get; set; }
        public string CountryOfOrigin { get; set; }
    }
}
