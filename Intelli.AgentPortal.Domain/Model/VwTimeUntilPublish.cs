using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class VwTimeUntilPublish
    {
        public string RequestId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? StartProcessDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string ProcessTimeUntilPublish { get; set; }
    }
}
