using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class CountriesPerCompany
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int BatchSourceId { get; set; }
        public int CountryId { get; set; }

        public virtual BatchSource BatchSource { get; set; }
        public virtual Company Company { get; set; }
        public virtual Country Country { get; set; }
    }
}
