
namespace Intelli.AgentPortal.Api.DTO
{
    /// <summary>
    /// The country DTO.
    /// </summary>
    public class CountryDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the country name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the 2 digit code.
        /// </summary>
        public string Code2D { get; set; }

        /// <summary>
        /// Gets or sets the 3 digit code.
        /// </summary>
        public string Code3D { get; set; }

        /// <summary>
        /// Gets or sets the mobile code.
        /// </summary>
        public string MobileCode { get; set; }

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
    }
}
