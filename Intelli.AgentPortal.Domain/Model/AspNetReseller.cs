using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class AspNetReseller
    {
        /// <summary>
        /// Gets or sets the system user id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the system user id.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the system user id.
        /// </summary>
        public int CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public virtual Company Company { get; set; }
    }
}
