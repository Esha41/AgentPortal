using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BopDictionary
    {
        public BopDictionary()
        {
            BatchItemFields = new HashSet<BatchItemField>();
        }

        public int Id { get; set; }
        public int DictionaryTypeId { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        public virtual DictionaryType DictionaryType { get; set; }
        public virtual ICollection<BatchItemField> BatchItemFields { get; set; }
    }
}
