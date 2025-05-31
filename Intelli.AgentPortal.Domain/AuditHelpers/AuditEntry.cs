using Intelli.AgentPortal.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Intelli.AgentPortal.Domain.Core.Helpers
{
    /// <summary>
    /// The audit entry.
    /// </summary>
    public class AuditEntry
    {
        /// <summary>
        /// Gets or sets the audit type.
        /// </summary>
        public AuditType AuditType { get; set; }

        /// <summary>
        /// Gets or sets the audit user.
        /// </summary>
        public string AuditUser { get; set; }

        /// <summary>
        /// Gets or sets the table name.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Gets the key values.
        /// </summary>
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets the old values.
        /// </summary>
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();


        /// <summary>
        /// Gets the new values.
        /// </summary>
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets the changed columns.
        /// </summary>
        public List<string> ChangedColumns { get; } = new List<string>();

        /// <summary>
        /// Gets or sets the request Id.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEntry"/> class.
        /// </summary>
        /// <param name="entry">The entity entry.</param>
        /// <param name="auditUser">The audit user.</param>
        /// <param name="requestId">The request Id.</param>
        public AuditEntry(EntityEntry entry, string auditUser, string requestId = null)
        {

            AuditUser = auditUser;
            RequestId = requestId;

            SetChanges(entry);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEntry"/> class.
        /// </summary>
        public AuditEntry()
        {
        }

        /// <summary>
        /// Sets the changes.
        /// </summary>
        private void SetChanges(EntityEntry entry)
        {
            TableName = entry.Metadata.GetTableName();

            var dbValues = entry.State == EntityState.Modified ? entry.GetDatabaseValues() : null;

            foreach (PropertyEntry property in entry.Properties)
            {
                string propertyName = property.Metadata.Name;
#pragma warning disable CS0618 // Type or member is obsolete
                string dbColumnName = property.Metadata.GetColumnName();
#pragma warning restore CS0618 // Type or member is obsolete

                if (property.Metadata.IsPrimaryKey())
                {
                    KeyValues[propertyName] = property.CurrentValue;
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        NewValues[propertyName] = property.CurrentValue;
                        AuditType = AuditType.Create;
                        break;

                    case EntityState.Deleted:
                        OldValues[propertyName] = property.OriginalValue;
                        AuditType = AuditType.Delete;
                        break;

                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            ChangedColumns.Add(dbColumnName);

                            OldValues[propertyName] = dbValues != null ? dbValues[propertyName] : property.OriginalValue;
                            NewValues[propertyName] = property.CurrentValue;

                            AuditType = AuditType.Update;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Creates an Audit object.
        /// </summary>
        /// <returns>An Audit object.</returns>
        public Audit ToAudit()
        {
            var audit = new Audit
            {
                AuditDateTimeUtc = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                AuditType = AuditType.ToString(),
                AuditUser = AuditUser,
                TableName = TableName,
                KeyValues = JsonConvert.SerializeObject(KeyValues),
                OldValues = OldValues.Count == 0 ?
                              string.Empty : JsonConvert.SerializeObject(OldValues),
                NewValues = NewValues.Count == 0 ?
                              string.Empty : JsonConvert.SerializeObject(NewValues),
                ChangedColumns = ChangedColumns.Count == 0 ?
                                   string.Empty : JsonConvert.SerializeObject(ChangedColumns),
                RequestId = RequestId,
            };

            return audit;
        }
    }
}
