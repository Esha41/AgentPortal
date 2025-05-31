using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class UserPreference
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int GridPageSize { get; set; }
        public int SystemUserId { get; set; }

        public virtual SystemUser SystemUser { get; set; }
    }
}
