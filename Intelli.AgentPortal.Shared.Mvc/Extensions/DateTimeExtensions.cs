
using System;
using System.Globalization;

namespace Intelli.AgentPortal.Shared.Mvc.Extensions
{
    /// <summary>
    /// DateExtensions class
    /// </summary>
    public static class DateExtensions
    {
        public const string SqlDateFormatString = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'";
        /// <summary>
        /// IsoDate
        /// </summary>
        /// <param name="dtDate">The dt date.</param>
        /// <param name="addPrefixZeros">if set to <c>true</c> [add prefix zeros].</param>
        /// <returns></returns>
        public static string IsoDate(this DateTime dtDate, bool addPrefixZeros)
        {
            var str1 = addPrefixZeros ? Extensions.AddPrefixZeroes(dtDate.Month.ToString(), 2) : dtDate.Month.ToString();
            var str2 = addPrefixZeros ? Extensions.AddPrefixZeroes(dtDate.Day.ToString(), 2) : dtDate.Day.ToString();
            return dtDate.Year + "-" + str1 + "-" + str2;
        }

        /// <summary>
        /// Isoes the year month.
        /// </summary>
        /// <param name="dtDateTime">The dt date time.</param>
        /// <param name="addPrefixZeros">if set to <c>true</c> [add prefix zeros].</param>
        /// <returns></returns>
        public static string IsoYearMonth(this DateTime dtDateTime, bool addPrefixZeros)
        {
            var str = addPrefixZeros
              ? Extensions.AddPrefixZeroes(dtDateTime.Month.ToString(), 2)
              : dtDateTime.Month.ToString();
            return dtDateTime.Year + "-" + str;
        }
        /// <summary>
        /// To the SQL date time.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string ToSqlDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString(SqlDateFormatString, CultureInfo.InvariantCulture);
        }

    }
}