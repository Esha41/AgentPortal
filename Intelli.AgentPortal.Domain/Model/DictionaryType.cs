using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class DictionaryType
    {
        public DictionaryType()
        {
            BopDictionaries = new HashSet<BopDictionary>();
        }

        public int Id { get; set; }
        public string DictionaryType1 { get; set; }

        public virtual ICollection<BopDictionary> BopDictionaries { get; set; }
    }
}
