using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class ViewBatchesToBeDeleted
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BatchStatusId { get; set; }
        public int BatchSourceId { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int? RetriesCount { get; set; }
        public bool? AppliedGdpr { get; set; }
        public int CompanyId { get; set; }
        public int VideoPublishRetriesCount { get; set; }
        public int VideoStatus { get; set; }
        public bool? SendResponseUpponGdpr { get; set; }
    }
}
