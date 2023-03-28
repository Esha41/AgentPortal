using System;

namespace Intelli.AgentPortal.Api.DTO
{
    public class VwDocumentGroupNamesForBatchHistoryDTO
    {
        public int Id { get; set; }
        public int BatchHistoryId { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string DocumentGroupName { get; set; }
        public bool? IsValid { get; set; }
        public string FileName { get; set; }
        public int? PageId { get; set; }
        public int? DocumentGroupNameId { get; set; }
        public bool? IsLast { get; set; }
    }
}
