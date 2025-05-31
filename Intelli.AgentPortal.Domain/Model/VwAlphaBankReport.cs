using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwAlphaBankReport
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string WaitingTime { get; set; }
        public string VideoCallTime { get; set; }
        public string TransactionTime { get; set; }
        public string BatchStatus { get; set; }
        public string BatchVideoStatus { get; set; }
        public string Agent { get; set; }
        public string FieldValue { get; set; }
    }
}
