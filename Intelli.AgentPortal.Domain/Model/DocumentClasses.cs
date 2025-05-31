using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class DocumentClasses
    {
        public DocumentClasses()
        {
            DocumentsPerCompany = new HashSet<DocumentsPerCompany>();
            BatchItems = new HashSet<BatchItem>();
            DocumentClassFields = new HashSet<DocumentClassField>();

        }

        public int Id { get; set; }
     //  public string DocumentClassName { get; set; }
       // public string DocumentClass1 { get; set; }
        public string DocumentClass { get; set; }
        public string EnumValue { get; set; }
        public string RecognitionMappedName { get; set; }
        public int? DocumentGroupNameId { get; set; }
        public string FriendlyName { get; set; }

        public virtual ICollection<DocumentsPerCompany> DocumentsPerCompany { get; set; }
        public virtual DocumentGroupName DocumentGroupName { get; set; }
        public virtual ICollection<BatchItem> BatchItems { get; set; }
        public virtual ICollection<DocumentClassField> DocumentClassFields { get; set; }
    }
}
