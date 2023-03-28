using Intelli.AgentPortal.Domain.Core.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Intelli.AgentPortal.Api.DTO.Audit
{
    /// <summary>
    /// The export log DTO.
    /// </summary>
    public class ExportLogDTO
    {
        private string _filterExpression;

        /// <summary>
        /// Gets the audit type.
        /// </summary>
        public AuditType AuditType { get; private set; } = AuditType.Export;

        /// <summary>
        /// Gets or sets the screen name.
        /// </summary>
        [Required]
        public string ScreenName { get; set; }

        /// <summary>
        /// Gets or sets the filter expression.
        /// </summary>
        [Required]
        public string FilterExpression
        {
            get
            {
                return _filterExpression;
            }
            set
            {
                try
                {
                    var data = Convert.FromBase64String(value.Replace(' ', '+'));
                    _filterExpression = Encoding.Default.GetString(data);
                }
                catch (FormatException)
                {
                    _filterExpression = string.Empty;
                }
            }
        }
    }
}
