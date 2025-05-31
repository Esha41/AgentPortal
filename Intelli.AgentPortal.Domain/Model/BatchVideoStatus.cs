using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchVideoStatus
    {
        public BatchVideoStatus()
        {
            Batches = new HashSet<Batch>();
        }

        public int Id { get; set; }
        public string BatchStatus { get; set; }
        public string EnumValue { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }
    }
}
