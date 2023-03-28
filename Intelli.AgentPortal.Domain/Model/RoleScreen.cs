using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class RoleScreen
    {
        public int Id { get; set; }
        public int SystemRoleId { get; set; }
        public int ScreenId { get; set; }
        public int Privilege { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual Screen Screen { get; set; }
        public virtual SystemRole SystemRole { get; set; }
    }
}
