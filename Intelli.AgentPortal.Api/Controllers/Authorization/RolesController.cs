using AutoMapper;
using Intelli.AgentPortal.Api.DTO;
using Intelli.AgentPortal.Domain.Core.Helpers;
using Intelli.AgentPortal.Domain.Core.Repository;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Domain.Repository;
using Intelli.AgentPortal.Domain.Repository.Impl;
using Intelli.AgentPortal.EventBus.RabbitMQ.Event;
using Intelli.AgentPortal.EventBus.RabbitMQ.Sender;
using Intelli.AgentPortal.Shared;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Controllers.v1
{
    /// <summary>
    /// The roles controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RolesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICompanyRepository<SystemRole> _rolesRepository;
        private readonly IRepository<Screen> _screensRepository;
        private readonly IRepository<RoleScreen> _roleScreensRepository;
        private readonly IRepository<RoleScreenElement> _roleScreenElementsRepository;
        private readonly IRepository<SystemUser> _systemUserRepository;
        private readonly IRepository<SystemUserRole> _systemUserRoleRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="RolesController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="sender">The Event Sender.</param>
        public RolesController(AgentPortalContext context, IMapper mapper,
            ILogger<RolesController> logger,
            IEventSender sender)
        {
            _rolesRepository = new CompanyEntityRepository<SystemRole>(context);
            _screensRepository = new GenericRepository<Screen>(context);
            _roleScreensRepository = new GenericRepository<RoleScreen>(context);
            _roleScreenElementsRepository = new GenericRepository<RoleScreenElement>(context);
            _systemUserRepository = new GenericRepository<SystemUser>(context);
            _systemUserRoleRepository = new GenericRepository<SystemUserRole>(context);


            ((GenericRepository<RoleScreen>)_roleScreensRepository).AfterSave =
            ((GenericRepository<RoleScreenElement>)_roleScreenElementsRepository).AfterSave =
            ((CompanyEntityRepository<SystemRole>)_rolesRepository).AfterSave = (logs) =>
                sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns>ActionResult of IEnumerable of Role</returns>
        [HttpGet]
        public ActionResult<IEnumerable<RoleDTO>> GetAll([FromQuery] QueryStringParams queryStringParams)
        {
            _logger.LogInformation("GetRoles Called with params: {0}", queryStringParams);
            PagedResult<RoleDTO> result = null;
            try
            {
                QueryResult<SystemRole> queryResult = new();
                if (!queryStringParams.IsExport)
                {
                    queryResult = _rolesRepository.Get(CompanyIds,
                                    queryStringParams.FilterExpression,
                                    queryStringParams.OrderBy,
                                    queryStringParams.PageSize,
                                    queryStringParams.PageNumber, x => x.Company);
                }
                else
                {
                    queryResult = _rolesRepository.Get(CompanyIds,
                                    queryStringParams.FilterExpression,
                                    queryStringParams.OrderBy,
                                    x => x.Company);
                }

                int total = queryResult.Count;
                IEnumerable<SystemRole> roleList = queryResult.List;

                result = new PagedResult<RoleDTO>(
                        total,
                        queryStringParams.PageNumber,
                        roleList.Select(x => _mapper.Map<RoleDTO>(x)).ToList(),
                        queryStringParams.PageSize
                    );
            }
            catch (ArgumentException e)
            {
                // Log error message
                _logger.LogError(e, "{0}", e.Message);

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
            return Ok(result);
        }

        /// <summary>
        /// Gets all active.
        /// </summary>
        /// <returns>Action Result of IEnumerable of Role.</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetActive()
        {
            var dto = new RoleDTO();


            var result = await _rolesRepository.GetAllActiveAsync(CompanyIds, nameof(dto.Priority));

            // If user is associated to any company then filter out the roles of those companies on the basis of priority
            // If user is super admin this check will not run and all data will be returned
            if(CompanyIds.Count() > 0)
            {
                var userRoles = _systemUserRepository.Query(x => x.Email == UserName).Select(x => x.Id);
                var systemUserRoles = _systemUserRoleRepository.Query(x => x.SystemUserId == userRoles.FirstOrDefault())
                                                               .Include(x => x.SystemRole)
                                                               .OrderBy(x => x.SystemRole.Priority)
                                                               .ToList().FirstOrDefault();

                // Filter all roles which has priority value higher than user's role.
                // Higher the value lower the priority of the role
                // 1 has higher priority than 2
                // Thus user could only create users with lower privileges.
                var filteredResult = result.List.FindAll(x => x.Priority > systemUserRoles.SystemRole.Priority).ToList();



                return Ok(new { Items = filteredResult.Select(x => _mapper.Map<RoleDTO>(x)).ToList() });
            }

            return Ok(new { Items = result.List.Select(x => _mapper.Map<RoleDTO>(x)).ToList() });
        }

        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task: ActionResult of Role.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SystemRole>> GetRole(int id)
        {
            RoleDTO dto = null;
            try
            {
                var query = _rolesRepository.Query(CompanyIds, x => x.Id == id)
                .Include(x => x.SystemUserRoles)
                .ThenInclude(x => x.SystemUser)
                .ThenInclude(x => x.UserCompanies)
                .ThenInclude(x => x.Company)
                .Include(x => x.RoleScreens)
                .Include(x => x.RoleScreenElements);

                var role = await query.FirstOrDefaultAsync();
                if (role == null) return NotFound();

                var screensQuery = _screensRepository.Query()
                    .Include(x => x.ScreenElements)
                    .OrderBy(x => x.Id);
                var screens = await screensQuery.ToListAsync();

                dto = _mapper.Map<RoleDTO>(role);

                dto.Users = role.SystemUserRoles.Select(x =>
                {
                    var dto = _mapper.Map<UserReadDTO>(x.SystemUser);
                    dto.setAssociatedCompanies();
                    return dto;
                }).ToList();

                dto.Screens = GetRoleScreensDtoList(role, screens);
                dto.companyRoles = _rolesRepository.Query(CompanyIds, x => x.CompanyId == dto.CompanyId)
                                                   .Include(x => x.Company)
                                                    .OrderBy(x => x.Priority).ToList()
                                                    .Select(x =>
                                                    {
                                                        var dto = _mapper.Map<SystemRole>(x);
                                                        dto.ReduceResponseSize();
                                                        return dto;
                                                    }).ToList();
            }
            catch (ArgumentException e)
            {
                // Log error message
                _logger.LogError(e, "{0}", e.Message);

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }

            return Ok(dto);
        }

        /// <summary>
        /// Gets the role screens data.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="screens">The screens.</param>
        /// <returns>A list of RoleScreenDTOS.</returns>
        private static List<RoleScreenDTO> GetRoleScreensDtoList(SystemRole role, List<Screen> screens)
        {
            return screens.Select(x =>
            {
                var roleScreen = role.RoleScreens.FirstOrDefault(y => y.ScreenId == x.Id);
                int roleScreenPrivilige = roleScreen != null ? roleScreen.Privilege : RoleScreen.NO_PRIVILEGE;

                return GetRoleScreenDto(role, x, roleScreenPrivilige);
            }).ToList();
        }

        /// <summary>
        /// Gets the role screen dto.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="screen">The x.</param>
        /// <param name="roleScreenPrivilige">The role screen privilige.</param>
        /// <returns>A RoleScreenDTO.</returns>
        private static RoleScreenDTO GetRoleScreenDto(SystemRole role, Screen screen, int roleScreenPrivilige)
        {
            return new RoleScreenDTO
            {
                ScreenName = screen.ScreenName,
                ScreenPriviliges = roleScreenPrivilige,
                RoleId = role.Id,
                ScreenId = screen.Id,
                ScreenElementPriviliges = GetRoleScreenElementDtoList(role, screen, roleScreenPrivilige),
            };
        }

        /// <summary>
        /// Gets the screen element priviliges.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="screen">The screen.</param>
        /// <param name="roleScreenPrivilige">The role screen privilige.</param>
        /// <returns>A list of RoleScreenElementDTOS.</returns>
        internal static List<RoleScreenElementDTO> GetRoleScreenElementDtoList(SystemRole role, Screen screen, int roleScreenPrivilige)
        {
            return screen.ScreenElements.Where(y => y.ScreenId == screen.Id).Select(y =>
            {
                int roleScreenElementPrivilige = GetRoleScreenElementPrivilege(role, roleScreenPrivilige, y.Id);
                return new RoleScreenElementDTO
                {
                    ElementName = y.ScreenElementName,
                    Priviliges = roleScreenElementPrivilige,
                    RoleId = role.Id,
                    ScreenElementId = y.Id,
                };
            }).ToList();
        }

        /// <summary>
        /// Gets the role screen element privilege.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="roleScreenPrivilige">The role screen privilige.</param>
        /// <param name="screenElementId">The screen element id.</param>
        /// <returns>An int.</returns>
        private static int GetRoleScreenElementPrivilege(SystemRole role, int roleScreenPrivilige, int screenElementId)
        {
            int roleScreenElementPrivilige;
            if (roleScreenPrivilige == RoleScreen.FULL_CONTROL)
            {
                roleScreenElementPrivilige = RoleScreenElement.FULL_CONTROL;
            }
            else if (roleScreenPrivilige == RoleScreen.NO_PRIVILEGE)
            {
                roleScreenElementPrivilige = RoleScreenElement.NO_PRIVILEGE;
            }
            else // Custom privilege
            {
                var roleScreenElement = role.RoleScreenElements.FirstOrDefault(z => z.ScreenElementId == screenElementId);
                roleScreenElementPrivilige = roleScreenElement != null ?
                                                roleScreenElement.Privilege :
                                                RoleScreenElement.NO_PRIVILEGE;
            }
            return roleScreenElementPrivilige;
        }

        /// <summary>
        /// Puts the role.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="roleDTO">The role d t o.</param>
        /// <returns>A Task: ActionResult</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, RoleDTO roleDTO)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || roleDTO == null || id != roleDTO.Id)
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }

            var role = _mapper.Map<SystemRole>(roleDTO);
            await using var trans = _rolesRepository.GetTransaction();
            try
            {
                _rolesRepository.Update(CompanyIds, role);

                if (roleDTO.Screens != null)
                {
                    foreach (var rscrDto in roleDTO.Screens)
                    {
                        _roleScreensRepository.Delete(x => x.SystemRoleId == role.Id
                                                            && x.ScreenId == rscrDto.ScreenId);
                        _roleScreensRepository.Insert(new RoleScreen
                        {
                            SystemRoleId = role.Id,
                            ScreenId = rscrDto.ScreenId,
                            Privilege = rscrDto.ScreenPriviliges,
                        });

                        if (rscrDto.ScreenElementPriviliges != null)
                        {
                            foreach (var releDto in rscrDto.ScreenElementPriviliges)
                            {
                                _roleScreenElementsRepository.Delete(x => x.RoleId == role.Id
                                                                            && x.ScreenElementId == releDto.ScreenElementId);
                                _roleScreenElementsRepository.Insert(new RoleScreenElement
                                {
                                    RoleId = role.Id,
                                    ScreenElementId = releDto.ScreenElementId,
                                    Privilege = releDto.Priviliges,
                                });
                            }
                        }
                    }
                }

                // Save changes
                _rolesRepository.SaveChanges(UserName, trans);
                _roleScreensRepository.SaveChanges(UserName, trans);
                _roleScreenElementsRepository.SaveChanges(UserName, trans);

                // Commit transaction
                await trans.CommitAsync();
            }
            catch (DbUpdateException e)
            {
                // Rollback transaction
                await trans.RollbackAsync();

                // Log error message
                _logger.LogError(e, "{0}", e.Message);

                // Show Error message
                return BadRequest(new
                {
                    Errors = e,
                    Message = MsgKeys.RoleDuplicated
                });
            }
            catch (Exception e)
            {
                // Rollback transaction
                await trans.RollbackAsync();

                // Log error message
                _logger.LogError(e, "{0}", e.Message);

                // Show Error message
                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }

            return Ok(_mapper.Map<RoleDTO>(role));
        }

        /// <summary>
        /// Posts the role.
        /// </summary>
        /// <param name="roleDTO">The roleDTO object.</param>
        /// <returns>Action Result of Role</returns>
        [HttpPost]
        public async Task<IActionResult> PostRole(RoleDTO roleDTO)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || roleDTO == null)
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }

            var role = _mapper.Map<SystemRole>(roleDTO);
            await using var trans = _rolesRepository.GetTransaction();
            try
            {
                _rolesRepository.Insert(CompanyIds, role);
                _rolesRepository.SaveChanges(UserName, trans);

                // Save screen privileges
                SaveScreenPrivileges(roleDTO, role.Id);

                // Save changes
                _roleScreensRepository.SaveChanges(UserName, trans);
                _roleScreenElementsRepository.SaveChanges(UserName, trans);

                // Commit transaction
                await trans.CommitAsync();
            }
            catch (DbUpdateException e)
            {
                // Rollback transaction
                await trans.RollbackAsync();

                // Log error message
                _logger.LogError(e, "{0}", e.Message);

                // Show Error message
                return BadRequest(new
                {
                    Errors = e,
                    Message = MsgKeys.RoleDuplicated
                });
            }
            catch (Exception e)
            {
                // Rollback transaction
                await trans.RollbackAsync();

                // Log error message
                _logger.LogError(e, "{0}", e.Message);

                // Show Error message
                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }

            return Ok(_mapper.Map<RoleDTO>(role));
        }

        /// <summary>
        /// Saves the screen privileges.
        /// </summary>
        /// <param name="roleDTO">The role DTO.</param>
        /// <param name="roleId">The role id.</param>
        private void SaveScreenPrivileges(RoleDTO roleDTO, int roleId)
        {
            if (roleDTO.Screens != null)
            {
                foreach (var screen in roleDTO.Screens)
                {
                    _roleScreensRepository.Insert(new RoleScreen
                    {
                        SystemRoleId = roleId,
                        ScreenId = screen.ScreenId,
                        Privilege = screen.ScreenPriviliges,
                    });

                    // Save screen element privileges
                    if (screen.ScreenElementPriviliges != null)
                    {
                        foreach (var element in screen.ScreenElementPriviliges)
                        {
                            _roleScreenElementsRepository.Insert(new RoleScreenElement
                            {
                                RoleId = roleId,
                                ScreenElementId = element.ScreenElementId,
                                Privilege = element.Priviliges,
                            });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Deletes the role.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Action Result of Role.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var query = _rolesRepository.Query(CompanyIds, x => x.Id == id)
                .Include(x => x.SystemUserRoles);

            var entity = await query.FirstOrDefaultAsync();
            if (entity == null) return NotFound();
            entity.IsActive = false;
            _rolesRepository.SaveChanges(UserName, null);

            return Ok(MsgKeys.DeletedSuccessfully);

         /*   if (entity.SystemUserRoles.Count == 0)
            {
                entity.IsActive = false;
                _rolesRepository.SaveChanges(UserName, null);

                return Ok(MsgKeys.DeletedSuccessfully);
            }

            return BadRequest(MsgKeys.ChildEntityExists);*/
        }
    }
}
