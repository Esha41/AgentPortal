using System;

namespace Intelli.AgentPortal.Api.DTO.Audit
{
    /// <summary>
    /// The audit entry converter.
    /// Converts business audits to audit entity.
    /// </summary>
    public static class AuditEntryConverter
    {
        /// <summary>
        /// Converts export log dto to audit entity.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="dto">The export log dto.</param>
        /// <returns>A Domain.Core.Model.Audit object.</returns>
        public static Domain.Model.Audit ToAudit(string userName, ExportLogDTO dto)
        {
            var audit = new Domain.Model.Audit
            {
                AuditDateTimeUtc = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                AuditType = dto.AuditType.ToString(),
                AuditUser = userName,
                TableName = dto.ScreenName,
                KeyValues = string.Empty,
                OldValues = string.Empty,
                NewValues = dto.FilterExpression,
                ChangedColumns = string.Empty,
                RequestId = string.Empty
            };
            return audit;
        }
    }
}
