using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class ScreenElement
    {
        public ScreenElement()
        {
            RoleScreenElements = new HashSet<RoleScreenElement>();
        }

        public int Id { get; set; }
        public string ScreenElementName { get; set; }
        public int ScreenId { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual Screen Screen { get; set; }
        public virtual ICollection<RoleScreenElement> RoleScreenElements { get; set; }
    }
}
