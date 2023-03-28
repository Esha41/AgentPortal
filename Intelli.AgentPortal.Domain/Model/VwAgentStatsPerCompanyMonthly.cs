using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwAgentStatsPerCompanyMonthly
    {
        public string MonthOfStartVideoCall { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public int? VideosCountForEachAgent { get; set; }
    }
}
