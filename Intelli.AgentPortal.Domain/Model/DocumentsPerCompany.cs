using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class DocumentsPerCompany
    {
        public int Id { get; set; }
        public int DocumentClassId { get; set; }
        public int DocumentGroupId { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual DocumentClasses DocumentClass { get; set; }
        public virtual DocumentGroupName DocumentGroup { get; set; }
    }
}
