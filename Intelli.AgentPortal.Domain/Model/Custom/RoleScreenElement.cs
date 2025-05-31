using Intelli.AgentPortal.Domain.Model.Core;

namespace Intelli.AgentPortal.Domain.Model
{
    /// <summary>
    /// The role screen element entity.
    /// </summary>
    public partial class RoleScreenElement : IEntity
    {
        /// <summary>
        /// No privilege.
        /// </summary>
        public const int NO_PRIVILEGE = 0;

        /// <summary>
        /// Read only privilege.
        /// </summary>
        public const int READ_ONLY = 1;

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
        /// Have read only privilege.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool HasReadOnlyPrivilege()
        {
            return Privilege == READ_ONLY;
        }

        /// <summary>
        /// Have full control privilege.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool HasFullControlPrivilege()
        {
            return Privilege == FULL_CONTROL;
        }
    }
}
