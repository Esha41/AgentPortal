using Intelli.AgentPortal.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace Intelli.AgentPortal.Domain.Core.Helpers
{
    /// <summary>
    /// The audit helper.
    /// </summary>
    public class AuditHelper
    {
        readonly DbContext Db;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditHelper"/> class.
        /// </summary>
        /// <param name="db">The auditable db context.</param>
        public AuditHelper(DbContext db)
        {
            Db = db;
        }

        /// <summary>
        /// Adds the audit logs.
        /// </summary>
        /// <param name="userName">The user name.</param>
        public List<AuditEntry> CreateAuditLogs(string userName, string requestId = null)
        {
            Db.ChangeTracker.DetectChanges();
            List<AuditEntry> auditEntries = new();

            foreach (EntityEntry entry in Db.ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                {
                    continue;
                }
                var auditEntry = new AuditEntry(entry, userName, requestId);
                auditEntries.Add(auditEntry);
            }

            return auditEntries;
        }
    }
}
