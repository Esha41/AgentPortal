using System.Collections.Generic;

namespace Intelli.AgentPortal.Domain.Core.Repository
{
    /// <summary>
    /// The query result of Entity.
    /// </summary>
    public class QueryResult<TEntity>
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the list.
        /// </summary>
        public List<TEntity> List { get; set; }
    }
}
