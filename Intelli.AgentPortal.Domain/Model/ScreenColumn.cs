using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class ScreenColumn
    {
        public ScreenColumn()
        {
            RoleScreenColumns = new HashSet<RoleScreenColumn>();
        }

        public int Id { get; set; }
        public int ScreenId { get; set; }
        public string ColumnName { get; set; }
        public int DefaultOrder { get; set; }
        public bool DefaultVisibility { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual Screen Screen { get; set; }
        public virtual ICollection<RoleScreenColumn> RoleScreenColumns { get; set; }
    }
}
