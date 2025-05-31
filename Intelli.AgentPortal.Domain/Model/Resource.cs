using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class Resource
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int ResourceLanguageId { get; set; }
        public string KeyDescription { get; set; }
        public int PageId { get; set; }
        public int? FlowId { get; set; }
        public string Value { get; set; }
        public bool IsDefault { get; set; }

        public virtual ResourceLanguage ResourceLanguage { get; set; }
    }
}
