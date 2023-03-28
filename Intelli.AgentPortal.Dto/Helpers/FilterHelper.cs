using System.Linq;
using System.Text;

namespace Intelli.AgentPortal.Shared
{
    /// <summary>
    /// The filter expression helper.
    /// </summary>
    public static class FilterHelper
    {
        /// <summary>
        /// The Quote character constant.
        /// </summary>
        const char QUOTE = '"';

        /// <summary>
        /// The Backslash character constant.
        /// </summary>
        const char BACKSLASH = '\\';

        /// <summary>
        /// The AND Logic.
        /// </summary>
        const string AND = "&&";

        /// <summary>
        /// The OR Logic.
        /// </summary>
        const string OR = "||";

        /// <summary>
        /// Gets the escaped expression.
        /// </summary>
        /// <param name="exp">The expression string to be escaped</param>
        /// <returns>Escaped String</returns>
        public static string GetEscaped(string exp)
        {
            if (exp.Count(e => e == QUOTE) > 2)
            {
                if (exp.Contains(AND)) return HandleMultipleFilters(exp, AND);
                if (exp.Contains(OR)) return HandleMultipleFilters(exp, OR);

                int f = exp.IndexOf(QUOTE);
                int l = exp.LastIndexOf(QUOTE);

                var sb = new StringBuilder();
                char[] array = exp.ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    if (i != f && i != l && array[i] == QUOTE)
                    {
                        sb.Append(BACKSLASH);
                        sb.Append(QUOTE);
                    }
                    else if (array[i] == BACKSLASH)
                    {
                        sb.Append(BACKSLASH);
                        sb.Append(BACKSLASH);
                    }
                    else
                    {
                        sb.Append(array[i]);
                    }
                }
                return sb.ToString();
            }
            else
            {
                return exp.Replace(@"\", @"\\");
            }
        }

        /// <summary>
        /// Handle multiple filters request.
        /// </summary>
        /// <param name="exp">The exp of multiple filters</param>
        /// <returns>Return parsed filters</returns>
        private static string HandleMultipleFilters(string exp, string splitter)
        {
            var sb = new StringBuilder();
            string[] filters = exp.Split(splitter);

            for (int i = 0; i < filters.Length; i++)
            {
                if (i > 0) sb.Append(splitter);
                sb.Append(GetEscaped(filters[i]));
            }

            return sb.ToString();
        }
    }
}
