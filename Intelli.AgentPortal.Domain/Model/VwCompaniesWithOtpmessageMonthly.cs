using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwCompaniesWithOtpmessageMonthly
    {
        public string MonthOfTransaction { get; set; }
        public string CompanyName { get; set; }
        public int? TransactionsPerCompanyPerMonth { get; set; }
    }
}
