using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    public partial class Configuration
    {
        public int Id { get; set; }
        public bool PasswordRequireNonAlphanumeric { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool PasswordRequireDigit { get; set; }
        public int PasswordRequiredLength { get; set; }
        public int RestrictLastUsedPasswords { get; set; }
        public int ForcePasswordChangeDays { get; set; }
        public bool IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
