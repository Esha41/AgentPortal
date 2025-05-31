using Intelli.AgentPortal.Domain.Model;
using System.Collections.Generic;

namespace Intelli.AgentPortal.Api.DTO
{
    public class GetVerificationResultDTO
    {
       // public int CompanyId { get; set; }
        public string Token { get; set; }
        public int BatchHistoryItemId { get; set; }
        public int CompanyId { get; set; }
        public DocumentClassDTO RelatedDocumentClass {get; set; }
        public List<VwGetBatchHistoryItemField> BatchHistoryItemFieldList {get; set; } = new List<VwGetBatchHistoryItemField>();
        public List<DocumentBase64DTO> Base64Images { get; set; } = new List<DocumentBase64DTO>();
        public List<int> CompayIdsIsoCode2Digit { get; set; } = new List<int>(); 
    }
}
