#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class SystemUserCompany
    {
        public int Id { get; set; }
        public int SystemUserId { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual Company Company { get; set; }
        public virtual SystemUser SystemUser { get; set; }
    }
}
