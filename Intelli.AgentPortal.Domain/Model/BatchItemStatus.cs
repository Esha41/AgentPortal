using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchItemStatus
    {
        public BatchItemStatus()
        {
            BatchItems = new HashSet<BatchItem>();
        }

        public int Id { get; set; }
        public string BatchItemStatus1 { get; set; }
        public string EnumValue { get; set; }

        public virtual ICollection<BatchItem> BatchItems { get; set; }
    }
}
