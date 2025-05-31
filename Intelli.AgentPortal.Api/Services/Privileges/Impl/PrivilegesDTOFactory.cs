using AutoMapper;
using Intelli.AgentPortal.Api.DTO;
using Intelli.AgentPortal.Domain.Model;
using System.Linq;

namespace Intelli.AgentPortal.Api.Services
{
    /// <summary>
    /// Factory class to build respective DTO counterparts.
    /// Required due to some special cases.
    /// </summary>
    public class PrivilegesDTOFactory
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for PrivilegesDTOFactory.
        /// </summary>
        /// <param name="mapper">IMapper interface to convert passed object using AutoMaper, if required.</param>
        public PrivilegesDTOFactory(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Builds default UserReadPrivilegesDTO w/out Domain.Core.Model.User.
        /// </summary>
        /// <returns>Created UserReadPrivilegesDTO object</returns>
        public UserReadPrivilegesDTO Build()
        {
            return new UserReadPrivilegesDTO
            {
                // Add default preferences if does not exists.
                Preferences = new(),

                // Add default screen priviliges if does not exists
                ScreenPriviliges = new()
            };
        }

        /// <summary>
        /// Builds UserReadPrivilegesDTO from Domain.Core.Model.User.
        /// </summary>
        /// <param name="user">The User Model to build from.</param>
        /// <returns>Created UserReadPrivilegesDTO object</returns>
        public UserReadPrivilegesDTO Build(SystemUser user)
        {
            var dto = _mapper.Map<UserReadPrivilegesDTO>(user);

            var pref = user.UserPreferences.FirstOrDefault();
            dto.Preferences = pref != null ? _mapper.Map<UserPreferencesDTO>(pref) : (new());

            var privilegesMergeFactory = new PrivilegesMergeFactory();
            foreach (var usrRole in user.SystemUserRoles)
            {
                if (usrRole.SystemRole is null || usrRole.SystemRole.IsActive is null or false)
                    continue;
                foreach (var (roleScreen, rs) in from roleScreen in usrRole.SystemRole.RoleScreens
                                                 let rs = Build(roleScreen)
                                                 select (roleScreen, rs))
                {
                    rs.ScreenElementPriviliges = (from RoleScreenElement element in usrRole.SystemRole.RoleScreenElements
                                                  where element.ScreenElement.ScreenId == roleScreen.ScreenId
                                                  select Build(element)).ToList();
                    privilegesMergeFactory.Add(rs);
                }
            }
            dto.ScreenPriviliges = privilegesMergeFactory.GetMerged().Where(x => x.HasAdminOrCustomPrivilege()).ToList();

            foreach (var item in dto.ScreenPriviliges)
            {
                if (item.ScreenPriviliges == RoleScreen.FULL_CONTROL)
                {
                    item.ScreenElementPriviliges = new System.Collections.Generic.List<RoleScreenElementDTO>();
                }
            }

            return dto;
        }

        /// <summary>
        /// Builds RoleScreenDTO from RoleScreen object.
        /// </summary>
        /// <param name="roleScreen">The RoleScreen Model to build from.</param>
        /// <returns>Created RoleScreenDTO object</returns>
        public RoleScreenDTO Build(RoleScreen roleScreen)
        {
            return new RoleScreenDTO()
            {
                //Id = roleScreen.Id,
                ScreenName = roleScreen.Screen.ScreenName,
                ScreenPriviliges = roleScreen.Privilege,
                RoleId = roleScreen.SystemRoleId,
                ScreenId = roleScreen.ScreenId
            };
        }

        /// <summary>
        /// Builds RoleScreenElementDTO from RoleScreenElement object.
        /// </summary>
        /// <param name="element">The RoleScreenElement Model to build from.</param>
        /// <returns>Created RoleScreenElementDTO object</returns>
        public RoleScreenElementDTO Build(RoleScreenElement element)
        {
            return new RoleScreenElementDTO()
            {
                ElementName = element.ScreenElement.ScreenElementName,
                Priviliges = element.Privilege,
                RoleId = element.RoleId,
                ScreenElementId = element.ScreenElementId
            };
        }
    }
}
