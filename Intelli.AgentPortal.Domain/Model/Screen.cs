using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class Screen
    {
        public Screen()
        {
            ColumnPreferences = new HashSet<ColumnPreference>();
            RoleScreens = new HashSet<RoleScreen>();
            ScreenColumns = new HashSet<ScreenColumn>();
            ScreenElements = new HashSet<ScreenElement>();
        }

        public int Id { get; set; }
        public string ScreenName { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual ICollection<ColumnPreference> ColumnPreferences { get; set; }
        public virtual ICollection<RoleScreen> RoleScreens { get; set; }
        public virtual ICollection<ScreenColumn> ScreenColumns { get; set; }
        public virtual ICollection<ScreenElement> ScreenElements { get; set; }
    }
}
