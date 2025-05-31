using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class Country
    {
        public Country()
        {
            AspNetUsers = new HashSet<AspNetUser>();
            CountriesPerCompanies = new HashSet<CountriesPerCompany>();
            SystemUserCountries = new HashSet<SystemUserCountry>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code2D { get; set; }
        public string Code3D { get; set; }
        public string MobileCode { get; set; }
        public string EnumValue { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual ICollection<CountriesPerCompany> CountriesPerCompanies { get; set; }
        public virtual ICollection<SystemUserCountry> SystemUserCountries { get; set; }
    }
}
