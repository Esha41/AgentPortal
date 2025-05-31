using Intelli.AgentPortal.Domain.Model;
using System.Collections.Generic;

namespace Intelli.AgentPortal.Api.DTO
{
    public class UploadedDocumentsViewDTO
    {
        public UploadedDocumentsViewDTO()
        {
            List<DboVwDocumentGroupNamesForBatchHistory> BatchHistoryItems = new List<DboVwDocumentGroupNamesForBatchHistory>();
            List<BatchItemVMDTO> BatchItems = new List<BatchItemVMDTO>();
            List<UploadedDocumentsDTO> SignedDocuments = new List<UploadedDocumentsDTO>();
        }
        public string Token { get; set; }
        public bool AllUploadedDocumentsExist { get; set; }
        public List<UploadedDocumentsDTO> SignedDocuments { get; set; }
        public List<DboVwDocumentGroupNamesForBatchHistory> BatchHistoryItems { get; set; }
        public List<BatchItemVMDTO> BatchItems { get; set; }
    }
}
