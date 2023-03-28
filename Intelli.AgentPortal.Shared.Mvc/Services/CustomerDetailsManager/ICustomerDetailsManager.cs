using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Shared.DTO;

namespace Intelli.AgentPortal.Shared.Mvc.Services.CustomerDetailsManager
{
    /// <summary>
    /// Interface for Customer Details Manager
    /// </summary>
    public interface ICustomerDetailsManager
    {
        /// <summary>
        /// Get Customer Model.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <returns>A CustomerModel.</returns>
        public CustomerModel GetCustomerModel(Batch batch);
    }
}
