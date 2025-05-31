using System;

namespace Intelli.AgentPortal.Api.DTO
{
    public class BatchItemViewDTO
    {
        public int BatchItemId { get; set; }
        public int BatchId { get; set; }
        public string BatchSourceCode { get; set; }
        public string DocumentGroupName { get; set; }
        public int DocumentGroupId { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DocumentClassId { get; set; }
        public bool IsValid { get; set; }
    }
}
