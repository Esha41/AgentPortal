using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class CompaniesMonitoringCofiguration
    {
        public int Id { get; set; }
        public string Endpoint { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Jwt { get; set; }
        public int? CompanyId { get; set; }
        public int LogsRetentionDays { get; set; }

        public virtual Company Company { get; set; }
    }
}
