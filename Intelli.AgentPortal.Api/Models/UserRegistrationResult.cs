using Intelli.AgentPortal.Domain.Model;
using Microsoft.AspNetCore.Identity;
using System;

namespace Intelli.AgentPortal.Auth.Api.Models
{
    /// <summary>
    /// The user registration result.
    /// </summary>
    public class UserRegistrationResult
    {
        /// <summary>
        /// Gets or sets the identity result.
        /// </summary>
        public IdentityResult IdentityResult { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        public SystemUser User { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }
    }
}
