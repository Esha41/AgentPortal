using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.DTO
{
    public class VerifyBatchByAgentDTO
    {
        public string RequestId { get; set; }
        public string VideoSessionResult { get; set; }
        public bool VideoValidity { get; set; }
        public string VideoVerificationStatus { get; set; }
        public string FaceMatching { get; set; }
        public bool VerificationStatus { get; set; }
        public bool ClientConsents { get; set; }
        public bool FreeWill { get; set; }
        public bool IsAlive { get; set; }
       // public BatchStatusDTO batchStatus { get; set; }
        public int batchStatusId { get; set; }
    }
}
