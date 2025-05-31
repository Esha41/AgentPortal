
namespace Intelli.AgentPortal.Shared.Mvc.Extensions
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds prefix zeroes.
        /// </summary>
        /// <param name="sSource">The s source.</param>
        /// <param name="maxDigitLength">Maximum length of the digit.</param>
        /// <returns></returns>
        public static string AddPrefixZeroes(string sSource, int maxDigitLength)
        {
            var str = sSource;
            for (var index = 0; index < maxDigitLength - sSource.Length; ++index)
                str = "0" + str;
            return str;
        }

    }
}