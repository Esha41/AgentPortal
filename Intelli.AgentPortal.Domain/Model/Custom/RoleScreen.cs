
using Intelli.AgentPortal.Domain.Model.Core;

namespace Intelli.AgentPortal.Domain.Model
{
    /// <summary>
    /// The role screen.
    /// </summary>
    public partial class RoleScreen : IEntity
    {
        /// <summary>
        /// No privilege.
        /// </summary>
        public const int NO_PRIVILEGE = 0;

        /// <summary>
        /// Custom privilege.
        /// </summary>
        public const int CUSTOM_PRIVILEGE = 1;

        /// <summary>
        /// Full control privilege.
        /// </summary>
        public const int FULL_CONTROL = 2;

        /// <summary>
        /// Have no privilege.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool HasNoPrivilege()
        {
            return Privilege == NO_PRIVILEGE;
        }

        /// <summary>
        /// Have admin / full control privilege.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool HasFullControlPrivilege()
        {
            return Privilege == FULL_CONTROL;
        }

        /// <summary>
        /// Have custom privilege.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool HasCustomPrivilege()
        {
            return Privilege == CUSTOM_PRIVILEGE;
        }

        /// <summary>
        /// Have admin or custom privilege.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool HasAdminOrCustomPrivilege()
        {
            return Privilege == FULL_CONTROL || Privilege == CUSTOM_PRIVILEGE;
        }
    }
}
