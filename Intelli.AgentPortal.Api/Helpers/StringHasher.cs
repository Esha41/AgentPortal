using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Helpers
{
    /// <summary>
    /// The string hasher.
    /// </summary>
    public static class StringHasher
    {
        /// <summary>
        /// Gets the SHA256 hash of provided text.
        /// </summary>
        /// <param name="value">The text to hash value.</param>
        /// <returns>A hash value as string.</returns>
        public static async Task<string> GetHashAsync(string value)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(value))
                    return string.Empty;

                using var sha = new SHA256Managed();
                var textData = Encoding.UTF8.GetBytes(value);
                var hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            });
        }
    }
}
