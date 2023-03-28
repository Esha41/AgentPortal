using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Domain.Model.Core
{
    public interface ICompanyEntity : IEntity
    {
        /// <summary>
        /// Gets or sets the company ids.
        /// </summary>

        public int CompanyId { get; set; }
    }
}
