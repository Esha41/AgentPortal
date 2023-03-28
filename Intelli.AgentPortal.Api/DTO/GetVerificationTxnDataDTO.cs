using Intelli.AgentPortal.Domain.Model;
using System.Collections.Generic;

namespace Intelli.AgentPortal.Api.DTO
{
    public class GetVerificationTxnDataDTO
    {
        public int BatchItemId { get; set; }
        public int CompanyId { get; set; }
        public string Token { get; set; }
        public List<VwGetBatchItemField> BatchItemFieldList { get; set; } = new List<VwGetBatchItemField>();
        public List<DocumentBase64DTO> Base64Images { get; set; } = new List<DocumentBase64DTO>();
        public List<int> CompayIdsIsoCode2Digit { get; set; } = new List<int>();
    }
}
