using System.Collections.Generic;

namespace Intelli.AgentPortal.Api.DTO
{
    public class SignedDocumentsDTO
    {
        public BatchDTO Batch { get; set; }
        public List<UploadedDocumentsDTO> SignedDocuments { get; set; }
    }
}
