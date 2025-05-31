namespace Intelli.AgentPortal.Api.DTO
{
    /// <summary>
    /// The document class DTO.
    /// </summary>
    public class DocumentClassDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the document class name.
        /// </summary>
        public string DocumentClassName { get; set; }

        /// <summary>
        /// Gets or sets the enum value.
        /// </summary>
        public string EnumValue { get; set; }

        /// <summary>
        /// Gets or sets the recognition mapped name.
        /// </summary>
        public string RecognitionMappedName { get; set; }

        /// <summary>
        /// Gets or sets the friendly name.
        /// </summary>
        public string FriendlyName { get; set; }

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