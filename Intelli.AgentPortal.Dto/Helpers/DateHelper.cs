using System;
using System.Globalization;

namespace Intelli.AgentPortal.Shared
{
    /// <summary>
    /// The date helper.
    /// </summary>
    public static class DateHelper
    {
        /// <summary>
        /// Date format
        /// </summary>
        const string DATE_FORMAT1 = "yyyy-MM-dd";
        /// <summary>
        /// Date format
        /// </summary>
        const string DATE_FORMAT2 = "yyyy/MM/dd";
        /// <summary>
        /// Date format
        /// </summary>
        const string DATE_FORMAT3 = "yyyy.MM.dd";

        /// <summary>
        /// converts datetime to date string using a format.
        /// </summary>
        /// <param name="value">The datetime value.</param>
        /// <returns>A formatted datetime string.</returns>
        public static string ToString(DateTime? value)
        {
            if (value != null)
                return value?.ToString(DATE_FORMAT1);
            return null;
        }

        /// <summary>
        /// Converts a string value to datetime value using predefined formats.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>A DateTime value.</returns>
        public static DateTime? FromString(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return DateTime.ParseExact(value,
                    new string[] { DATE_FORMAT1, DATE_FORMAT2, DATE_FORMAT3 },
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None
                    );
            }
            return null;
        }
    }
}
