using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class ResourceLanguage
    {
        public ResourceLanguage()
        {
            Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public string Language { get; set; }
        public string Cultrure { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
