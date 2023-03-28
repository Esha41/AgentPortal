using System;
using System.Collections.Generic;

namespace Intelli.AgentPortal.Api.DTO
{
    public class BatchDTO
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CompanyId { get; set; }
        public int BatchStatusId { get; set; }
        public DateTime? PublishedDate { get; set; }
        public virtual BatchStatusDTO BatchStatus { get; set; }
        public virtual BatchCompaniesDTO Company { get; set; }
        public virtual ICollection<BatchMetumDTO> BatchMeta { get; set; }
    }
}
