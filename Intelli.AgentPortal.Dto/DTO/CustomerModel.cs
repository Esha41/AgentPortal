using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Shared.DTO
{
    public class CustomerModel
    {
        public string FirstName { set; get; }
        public string LastName { get; set; }
        public string Mobile { set; get; }
        public string TaxCode { get; set; }
        public string IDNumber { set; get; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string BirthDate { get; set; }
    }
}
