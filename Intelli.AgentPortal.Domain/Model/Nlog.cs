using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class Nlog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? PosId { get; set; }
        public DateTime Logged { get; set; }
        public string Level { get; set; }
        public string ClassName { get; set; }
        public string Message { get; set; }
        public string Stacktrace { get; set; }
        public string Exception { get; set; }
        public string Data { get; set; }
        public string RequestId { get; set; }
    }
}
