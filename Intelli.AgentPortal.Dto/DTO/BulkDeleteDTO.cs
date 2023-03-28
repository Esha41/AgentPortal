using System.ComponentModel.DataAnnotations;

namespace Intelli.AgentPortal.Shared.Dto
{
    /// <summary>
    /// The bulk delete DTO used to delete multiple rows.
    /// </summary>
    public class BulkDeleteDto
    {
        /// <summary>
        /// Gets or sets the list of ids of entities to be deleted.
        /// </summary>
        [Required]
        public int[] Ids { get; set; }

        /// <summary>
        /// Checks if object has data.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool HasData()
        {
            return Ids != null && Ids.Length > 0;
        }
    }
}
