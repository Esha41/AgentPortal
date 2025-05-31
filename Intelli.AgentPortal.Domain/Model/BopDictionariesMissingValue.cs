using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class BopDictionariesMissingValue
    {
        public int Id { get; set; }
        public int DictionaryTypeId { get; set; }
        public string Value { get; set; }
    }
}
