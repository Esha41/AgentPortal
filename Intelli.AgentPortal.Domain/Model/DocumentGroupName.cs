using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class DocumentGroupName
    {
        public DocumentGroupName()
        {
            DocumentsPerCompany = new HashSet<DocumentsPerCompany>();
            BatchSourceUploadDocs = new HashSet<BatchSourceUploadDoc>();
            DocumentClasses = new HashSet<DocumentClasses>();
        }

        public int Id { get; set; }
        public string DocumentGroupName1 { get; set; }
        public string Code { get; set; }

        public virtual ICollection<DocumentsPerCompany> DocumentsPerCompany { get; set; }
        public virtual ICollection<BatchSourceUploadDoc> BatchSourceUploadDocs { get; set; }
        public virtual ICollection<DocumentClasses> DocumentClasses { get; set; }
    }
}
