using Intelli.AgentPortal.Domain.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Domain.Core.Repository
{
    /// <summary>
    /// The transaction handler interface.
    /// </summary>
    public interface ITransactionHandler : IAsyncDisposable
    {
        /// <summary>
        /// Gets the logs.
        /// </summary>
        List<AuditEntry> Logs { get; }

        /// <summary>
        /// Commits the transactions asynchronously.
        /// </summary>
        /// <returns>A Task.</returns>
        Task CommitAsync();

        /// <summary>
        /// Rollbacks the transactions asynchronously.
        /// </summary>
        /// <returns>A Task.</returns>
        Task RollbackAsync();
    }
}
