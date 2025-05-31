using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class WebpagesRole
    {
        public WebpagesRole()
        {
            WebpagesUsersInRoles = new HashSet<WebpagesUsersInRole>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<WebpagesUsersInRole> WebpagesUsersInRoles { get; set; }
    }
}
