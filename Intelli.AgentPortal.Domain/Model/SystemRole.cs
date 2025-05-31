using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class SystemRole
    {
        public SystemRole()
        {
            RoleScreenColumns = new HashSet<RoleScreenColumn>();
            RoleScreenElements = new HashSet<RoleScreenElement>();
            RoleScreens = new HashSet<RoleScreen>();
            SystemUserRoles = new HashSet<SystemUserRole>();
        }

        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int? Priority { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<RoleScreenColumn> RoleScreenColumns { get; set; }
        public virtual ICollection<RoleScreenElement> RoleScreenElements { get; set; }
        public virtual ICollection<RoleScreen> RoleScreens { get; set; }
        public virtual ICollection<SystemUserRole> SystemUserRoles { get; set; }
    }
}
