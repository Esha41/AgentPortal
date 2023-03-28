using System;
using System.Text;
using System.Text.Json;

namespace Intelli.AgentPortal.Shared
{
    /// <summary>
    /// The paging request params.
    /// These will be used for Get Requests to return paged results.
    /// </summary>
    public class QueryStringParams
    {
        /// <summary>
        /// The max page size.
        /// </summary>
        private const int maxPageSize = 50;

        /// <summary>
        /// Default page size.
        /// </summary>
        private int _pageSize = maxPageSize;

        /// <summary>
        /// Filter expression.
        /// </summary>
        private string _filterExpression;

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the order by.
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the Is Export flag.
        /// </summary>
        public bool IsExport { get; set; } = false;

        /// <summary>
        /// Gets or sets the filter expression.
        /// </summary>
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
                    byte[] data = Convert.FromBase64String(value.Replace(' ', '+'));
                    _filterExpression = Encoding.Default.GetString(data);
                    _filterExpression = FilterHelper.GetEscaped(_filterExpression);
                }
                catch (FormatException)
                {
                    _filterExpression = string.Empty;
                }
            }
        }

        /// <summary>
        /// Sets the filter expression.
        /// </summary>
        /// <param name="filterExpression">The filter expression.</param>
        public void SetFilterExpression(string filterExpression)
        {
            _filterExpression = filterExpression;
        }

        public void SetOrderBy(string OrderBy)
        {
            this.OrderBy = OrderBy;
        }

        /// <summary>
        /// Overriding the ToString to generate string representation of the object
        /// Mainly to be used to logging.
        /// </summary>
        /// <returns>JSON string of teh current object.</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryStringParams"/> class.
        /// By Default every list will be sorted based on the primary key
        /// Since the primary key is auto increment we'll have the latest records at the top
        /// If a use case needs to change this behaviour it can extend this class
        /// </summary>
        public QueryStringParams()
        {
            OrderBy = "Id desc";
        }
    }
}
