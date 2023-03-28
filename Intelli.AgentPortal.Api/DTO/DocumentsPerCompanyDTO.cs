namespace Intelli.AgentPortal.Api.DTO
{
    /// <summary>
    /// The documents per company DTO.
    /// </summary>
    public class DocumentsPerCompanyDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the document class id.
        /// </summary>
        public int DocumentClassId { get; set; }

        /// <summary>
        /// Gets or sets the document group id.
        /// </summary>
        public int DocumentGroupId { get; set; }

        /// <summary>
        /// Gets or sets the company id.
        /// </summary>
        public int CompanyId { get; set; }

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
        /// Gets or sets the document class.
        /// </summary>
        public virtual DocumentClassDTO DocumentClass { get; set; }

        /// <summary>
        /// Gets or sets the document group.
        /// </summary>
        public virtual DocumentGroupNameDTO DocumentGroup { get; set; }
    }
}