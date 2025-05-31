namespace Intelli.AgentPortal.Api.DTO
{
    /// <summary>
    /// The users per company data transfer object.
    /// </summary>
    public class UsersPerCompanyDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the allowed users.
        /// </summary>
        public int AllowedUsers { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        public long CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        public long UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the company id.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        public CompanyDTO Company { get; set; }
    }
}
