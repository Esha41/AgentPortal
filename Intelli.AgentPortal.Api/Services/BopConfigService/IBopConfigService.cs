
namespace Intelli.AgentPortal.Api.Services.BopConfigService
{
    /// <summary>
    /// Interface for Bop Config Service
    /// </summary>
    public interface IBopConfigService
    {
        /// <summary>
        /// Get BopConfig By EnumValue.
        /// </summary>
        /// <param name="enumValue">The enumValue.</param>
        /// <returns>string.</returns>\
        string GetBopConfigByEnumValue(string enumValue);
    }
}
