namespace Intelli.AgentPortal.Api.DTO
{
    /// <summary>
    /// The document group name DTO.
    /// </summary>
    public class DocumentGroupNameDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the document group name1.
        /// </summary>
        public string DocumentGroupName1 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active or not.
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
    }
}