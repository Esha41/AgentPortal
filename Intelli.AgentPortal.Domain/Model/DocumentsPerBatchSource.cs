using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class DocumentsPerBatchSource
    {
        public int DocumentClassId { get; set; }
        public int DocumentGroupNameId { get; set; }
        public int? BatchSourceId { get; set; }
        public int CompanyId { get; set; }

        public virtual BatchSource BatchSource { get; set; }
        public virtual Company Company { get; set; }
        public virtual DocumentClasses DocumentClass { get; set; }
        public virtual DocumentGroupName DocumentGroupName { get; set; }
    }
}
