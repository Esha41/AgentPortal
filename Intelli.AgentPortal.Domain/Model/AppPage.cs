using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class AppPage
    {
        public AppPage()
        {
            BatchSourceAppPages = new HashSet<BatchSourceAppPage>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string ControllerPathAction { get; set; }
        public string FriendlyName { get; set; }

        public virtual ICollection<BatchSourceAppPage> BatchSourceAppPages { get; set; }
    }
}
