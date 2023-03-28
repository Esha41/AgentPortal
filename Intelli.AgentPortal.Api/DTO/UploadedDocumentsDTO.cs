using System;

namespace Intelli.AgentPortal.Api.DTO
{
    public class UploadedDocumentsDTO
    {
        public byte[] Base64ByteArray { get; set; }

        public string NameWithoutExtention { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsSigned { get; set; }

        public string LabelName { get; set; }

        public string FriendlyName { get; set; } // its the template name

        public string Base64String { get; set; }

        public BatchDTO Batch { get; set; }
    }
}
