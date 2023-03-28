

#nullable disable

using Intelli.AgentPortal.Domain.Model.Custom;
using System;
using System.Collections.Generic;

namespace Intelli.AgentPortal.Domain.Model
{
    /// <summary>
    /// The asp net user.
    /// </summary>
    public partial class AspNetUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the system user id.
        /// </summary>
        public int SystemUserId { get; set; }
        /// <summary>
        /// Gets or sets the system user id.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the system user id.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the system user.
        /// </summary>
        public SystemUser SystemUser { get; set; }

        public string FullName { get; set; }

        public string Jmbg { get; set; }


        public string ResetToken { get; set; }

      

        public int? NativeLanguageId { get; set; }

        public virtual Country NativeLanguage { get; set; }

        public virtual ICollection<BatchItemField> BatchItemFields { get; set; }
        public virtual ICollection<WebpagesUsersInRole> WebpagesUsersInRoles { get; set; }
    }   
}
