using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class AdvancedLogging
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string RequestId { get; set; }
        public string Ip { get; set; }
        public string Browser { get; set; }
        public string Device { get; set; }
        public string System { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool? ActionCompletion { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string RequestUrl { get; set; }
        public string RequestPayload { get; set; }
        public DateTime? RequestTime { get; set; }
        public string ResponceStatus { get; set; }
        public string ResponceError { get; set; }
        public string ResponcePayload { get; set; }
        public DateTime? ResponceTime { get; set; }
        public DateTime? ExitDate { get; set; }
    }
}
