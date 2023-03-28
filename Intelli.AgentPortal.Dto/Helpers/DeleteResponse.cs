
namespace Intelli.AgentPortal.Shared
{
    /// <summary>
    /// The delete response for bulk delete.
    /// </summary>
    public class DeleteResponse
    {
        /// <summary>
        /// Gets or sets the deleted items count.
        /// </summary>
        public int Deleted { get; set; }

        /// <summary>
        /// Gets or sets the total items requested to delete.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the ids of entities which are deleted.
        /// </summary>
        public int[] Ids { get; set; }
    }
}
