using Intelli.AgentPortal.Domain.Model;
using System.Collections.Generic;

namespace Intelli.AgentPortal.Api.DTO
{
    public class AgentVideoPageDTO
    {
        public List<DboVwDocumentGroupNamesForBatchHistory> batchHistoryItems { get; set; } = new List<DboVwDocumentGroupNamesForBatchHistory>();
        public List<BatchSourceUploadDoc> batchSourceUploadDocs { get; set; } = new List<BatchSourceUploadDoc>();
        public List<BatchItemViewDTO> batchItemsViewModel { get; set; } = new List<BatchItemViewDTO>();
        public int LastBatchHistoryId { get; set; }
        public bool? VerificationStatus { get; set; }
        public string VerificationRejectionReason { get; set; }
        public string FaceMatching { get; set; }
        public string IsAlive { get; set; }
        public string Logo { get; set; }
        public string SelfiePortraitSimilarity { get; set; }
    }
}
