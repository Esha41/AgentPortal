using System.Collections.Generic;

namespace Intelli.AgentPortal.Api.DTO
{
    public class VerificationResultsDTO
    {
        public string Token { get; set; }
        public BatchDTO Batch { get; set; }
        public string FaceMatching { get; set; }
        public string IsAlive { get; set; }
        public string VerificationStatus { get; set; }
        public List<string> VerificationRejectionReasons { get; set; }
    }
}
