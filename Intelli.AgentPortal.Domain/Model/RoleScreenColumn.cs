using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class RoleScreenColumn
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int ScreenColumnId { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual SystemRole Role { get; set; }
        public virtual ScreenColumn ScreenColumn { get; set; }
    }
}
