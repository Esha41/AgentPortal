
namespace Intelli.AgentPortal.Api.Services.BatchStatusService
{
    /// <summary>
    /// Interface for Batch Status Service
    /// </summary>
    public interface IBatchStatusService
    {
        /// <summary>
        /// Get Batch Status Id By EnumValue.
        /// </summary>
        /// <param name="enumValue">The enumValue.</param>
        /// <returns>int.</returns>\
        int GetBatchStatusIdByEnumValue(string enumValue);
    }
}
