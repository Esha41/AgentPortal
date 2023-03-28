using Intelli.AgentPortal.Domain.Model.Core;

#nullable disable

namespace Intelli.AgentPortal.Domain.Model
{
    /// <summary>
    /// The system user.
    /// </summary>
    public partial class SystemUser : IEntity
    {
        

        /// <summary>
        /// Is default system user?
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>A bool.</returns>
        public static bool IsDefault(int id)
        {
            return id == 1;
        }
    }
}
