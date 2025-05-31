using System;

namespace Intelli.AgentPortal.Api.DTO
{
    public class PendingBatchesVideoPriorityDTO
    {
        public int Id { get; set; }
        public int Priotity { get; set; }
        public int EstimatedTimeInMin { get; set; }
        public int? CompanyId { get; set; }
        public int? MaxCallTime { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? BatchStatusId { get; set; }
        public string RequestId { get; set; }
        public int? LockedBy { get; set; }
        public string CountryOfOrigin { get; set; }
        public PendingBatchesUserDTO User { get; set; } = new();
        public PendingBatchescCompanyDTO Company { get; set; } = new();
    }
}
