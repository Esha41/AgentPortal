using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BatchSourceUploadDoc
    {
        public int Id { get; set; }
        public int BatchSourceId { get; set; }
        public int DocumentGroupNameId { get; set; }
        public bool Mandatory { get; set; }
        public virtual BatchSource BatchSource { get; set; }
        public virtual DocumentGroupName DocumentGroupName { get; set; }
    }
}
