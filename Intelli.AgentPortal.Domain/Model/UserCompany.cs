using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class UserCompany
    {
        public int Id { get; set; }
        public int SystemUserId { get; set; }
        public int CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual Company Company { get; set; }
        public virtual SystemUser SystemUser { get; set; }
    }
}
