using Intelli.AgentPortal.Domain.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Domain.Core.Repository
{
    /// <summary>
    /// The transaction handler.
    /// </summary>
    public class TransactionHandler : ITransactionHandler
    {
        public Action<object> TransactionCommitted = null;

        /// <summary>
        /// Gets the logs.
        /// </summary>
        public List<AuditEntry> Logs { get; private set; } = new List<AuditEntry>();

        private readonly IDbContextTransaction _trans;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public TransactionHandler(DbContext context)
        {
            _trans = context.Database.BeginTransaction();
        }

        /// <summary>
        /// Commits the transaction asynchronously.
        /// </summary>
        /// <returns>A Task.</returns>
        public async Task CommitAsync()
        {
            await _trans.CommitAsync();
            TransactionCommitted?.Invoke(Logs);
        }

        /// <summary>
        /// Rollbacks the transaction asynchronously.
        /// </summary>
        /// <returns>A Task.</returns>
        public async Task RollbackAsync()
        {
            await _trans.RollbackAsync();
            Logs = new List<AuditEntry>();
        }

        /// <summary>
        /// Disposes the transaction asynchronously.
        /// </summary>
        /// <returns>A ValueTask.</returns>
        public async ValueTask DisposeAsync()
        {
            await _trans.DisposeAsync();
        }
    }
}