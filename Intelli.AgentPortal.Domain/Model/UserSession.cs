using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class UserSession
    {
        public int SystemUserId { get; set; }
        public string SessionId { get; set; }

        public virtual SystemUser SystemUser { get; set; }
    }
}
