using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Services.Cache
{
    /// <summary>
    /// The generic cache interface.
    /// </summary>
    public interface IGenericCache<T>
    {
        /// <summary>
        /// Checks if cache has the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>True, if has value, else false.</returns>
        Task<bool> HasValueAsync(int key);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A value of type T.</returns>
        Task<T> GetValueAsync(int key);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        Task SetValueAsync(int key, T value);
    }
}