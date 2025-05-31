using Intelli.AgentPortal.Domain.Model;

namespace Intelli.AgentPortal.Api.DTO
{
    public class BatchSourceUploadDocDTO
    {
        public int Id { get; set; }
        public int BatchSourceId { get; set; }
        public int DocumentGroupNameId { get; set; }
        public bool Mandatory { get; set; }
        public DocumentGroupNameDTO DocumentGroupName { get; set; }
    }
}
