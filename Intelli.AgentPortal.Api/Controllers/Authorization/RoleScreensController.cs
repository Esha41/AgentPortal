using AutoMapper;
using Intelli.AgentPortal.Api.DTO;
using Intelli.AgentPortal.Api.Services;
using Intelli.AgentPortal.Domain.Core.Helpers;
using Intelli.AgentPortal.Domain.Core.Repository;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.EventBus.RabbitMQ.Event;
using Intelli.AgentPortal.EventBus.RabbitMQ.Sender;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Controllers.v1
{
    /// <summary>
    /// The RoleScreens Controller.
    /// This controller is responsible for providing list of screens and their related screen elements by user role.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoleScreensController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IRepository<RoleScreen> _roleScreensRepository;
        private readonly IRepository<RoleScreenElement> _roleElementsRepository;
        private readonly IRepository<ScreenElement> _screenElementsRepository;
        private readonly IRepository<Screen> _screenRepository;
        private readonly IRepository<SystemRole> _systemRoleRepository;

        private readonly PrivilegesDTOFactory _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleScreensController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="AgentPortalContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        public RoleScreensController(AgentPortalContext context,
            IMapper mapper,
            IEventSender sender)
        {
            _screenRepository = new GenericRepository<Screen>(context);
            _systemRoleRepository = new GenericRepository<SystemRole>(context);
            _roleScreensRepository = new GenericRepository<RoleScreen>(context);
            _roleElementsRepository = new GenericRepository<RoleScreenElement>(context);
            _screenElementsRepository = new GenericRepository<ScreenElement>(context);

            ((GenericRepository<ScreenElement>)_screenElementsRepository).AfterSave =
            ((GenericRepository<RoleScreen>)_roleScreensRepository).AfterSave =
            ((GenericRepository<RoleScreenElement>)_roleElementsRepository).AfterSave = (logs) =>
            {
                sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));
            };

            _mapper = mapper;
            _factory = new PrivilegesDTOFactory(_mapper);
        }

        /// <summary>
        /// Gets all active.
        /// </summary>
        /// <returns>Action Result of IEnumerable.</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _roleScreensRepository.GetAllActiveAsync("Id desc");
            return Ok(new { Items = result.List.Select(x => _mapper.Map<RoleScreenDTO>(x)).ToList() });
        }

        /// <summary>
        /// Get List of RoleScreenDTO by specific role id.
        /// </summary>
        /// <param name="roleId">User's Role Id on the basis of which RoleScreenDTOs are required.</param>
        /// <returns>ActionResult of RoleScreenDTOs list in JSON format</returns>
        [HttpGet]
        [Route("{roleId}")]
        public async Task<IActionResult> GetScreensByRoleId(int roleId)
        {
            var query = _roleScreensRepository
                .Query(r => r.SystemRoleId == roleId)
                .Include(x => x.Screen);

            var list = await query.ToListAsync();

            return Ok(new { Items = list.Select(x => _factory.Build(x)).ToList() });
        }

        /// <summary>
        /// Get List of RoleScreenElementDTO by specific role id.
        /// </summary>
        /// <param name="roleId">User's Role Id on the basis of which RoleScreenElementDTOs are required.</param>
        /// <returns>ActionResult of RoleScreenElementDTOs list in JSON format</returns>
        [HttpGet]
        [Route("Elements/{roleId}")]
        public async Task<ActionResult<IEnumerable<RoleScreenElementDTO>>> GetElementsByRoleId(int roleId)
        {
            IQueryable<RoleScreenElement> q = _roleElementsRepository
                .Query(r => r.RoleId == roleId)
                .Include(x => x.ScreenElement);

            IEnumerable<RoleScreenElement> list = await q.ToListAsync();

            return Ok(new { Items = list.Select(x => _factory.Build(x)).ToList() });
        }

        /// <summary>
        /// Get List of RoleScreenElementDTO by specific role id and screen id.
        /// </summary>
        /// <param name="roleId">User's Role Id on the basis of which RoleScreenElementDTOs are required.</param>
        /// <param name="screenId">Screen Id on the basis of which RoleScreenElementDTOs are required.</param>
        /// <returns>ActionResult of ScreensDTO list in JSON format</returns>
        [HttpGet]
        [Route("Elements/{roleId}/{screenId}")]
        public async Task<IActionResult> GetElementsByRoleAndScreenId(int roleId, int screenId)
        {
            var roleQuery = _systemRoleRepository.Query(r => r.Id == roleId)
                .Include(x => x.RoleScreens)
                .Include(x => x.RoleScreenElements);
            var screenQuery = _screenRepository.Query(r => r.Id == screenId).Include(x => x.ScreenElements);

            var role = await roleQuery.FirstOrDefaultAsync();
            var screen = await screenQuery.FirstOrDefaultAsync();
            int? roleScreenPrivilige = role.RoleScreens.FirstOrDefault(x => x.ScreenId == screenId)?.Privilege;

            return Ok(new { Items = RolesController.GetRoleScreenElementDtoList(role, screen, roleScreenPrivilige ?? 0) });
        }

        /// <summary>
        /// Adds New RoleScreen
        /// </summary>
        /// <param name="dto">Fields wrapped in RoleScreenDTO which are to be inserted.</param>
        /// <returns>If Successfull returns the RoleScreenDTO object</returns>
        [HttpPost]
        public async Task<IActionResult> PostRoleScreen(RoleScreenDTO dto)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }

            RoleScreen roleScreen = _mapper.Map<RoleScreen>(dto);
            roleScreen.IsActive = true;
            _roleScreensRepository.Insert(roleScreen);
            _roleScreensRepository.SaveChanges(UserName, null);

            IQueryable<RoleScreen> q = _roleScreensRepository
                .Query(r => r.Id == roleScreen.Id)
                .Include(x => x.Screen);
            RoleScreen objFromDB = await q.FirstOrDefaultAsync();

            return Ok(_factory.Build(objFromDB));
        }

        /// <summary>
        /// Adds New RoleScreen privileges information for the role after deleting old ones.
        /// </summary>
        /// <param name="roleId">role id of the role for which privileges are being updated.</param>
        /// <param name="dtos">Fields wrapped in RoleScreenDTO List which are to be inserted.</param>
        /// <returns>If Successfull returns success message.</returns>
        [HttpPost]
        [Route("BulkInsert/{roleId}")]
        public async Task<IActionResult> BulkInsert(int roleId, List<RoleScreenDTO> dtos)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid
                || dtos == null
                || dtos.Exists(d => d.RoleId != roleId)
                )
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }

            await using (var trans = _roleScreensRepository.GetTransaction())
            {
                try
                {
                    _roleScreensRepository.Delete(x => x.SystemRoleId == roleId);
                    foreach (RoleScreenDTO dto in dtos)
                    {
                        RoleScreen rs = _mapper.Map<RoleScreen>(dto);
                        rs.Privilege = dto.ScreenPriviliges;
                        _roleScreensRepository.Insert(rs);
                    }
                    _roleScreensRepository.SaveChanges(UserName, trans);

                    // Commit transaction
                    await trans.CommitAsync();
                }
                catch (Exception e)
                {
                    // Rollback transaction
                    await trans.RollbackAsync();

                    // Show Error message
                    return BadRequest(new
                    {
                        Errors = e,
                        e.Message
                    });
                }
            }
            return Ok(MsgKeys.UpdatedSuccessfully);
        }

        /// <summary>
        /// Adds New RoleScreenElement
        /// </summary>
        /// <param name="dto">Fields wrapped in RoleScreenElementDTO which are to be inserted.</param>
        /// <returns>If Successful returns the RoleScreenElementDTO object</returns>
        [HttpPost]
        [Route("Elements")]
        public async Task<IActionResult> PostRoleScreenElement(RoleScreenElementDTO dto)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }

            var entity = await _roleElementsRepository
                .Query(x => x.RoleId == dto.RoleId && x.ScreenElementId == dto.ScreenElementId)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.Privilege = dto.Priviliges;
                entity.IsActive = true;
                _roleElementsRepository.Update(entity);
            }
            else
            {
                var element = _mapper.Map<RoleScreenElement>(dto);
                element.IsActive = true;
                _roleElementsRepository.Insert(element);
            }

            _roleElementsRepository.SaveChanges(UserName, null);

            return Ok(MsgKeys.UpdatedSuccessfully);
        }

        /// <summary>
        /// Adds New RoleScreenElement privileges information for the role after deleting old ones.
        /// </summary>
        /// <param name="roleId">Role id of the role for which privileges are being updated.</param>
        /// <param name="screenId">Screen id of the elements for which privileges are being updated.</param>
        /// <param name="dtos">Fields wrapped in RoleScreenElementDTO List which are to be inserted.</param>
        /// <returns>If Successfull returns success message.</returns>
        [HttpPost]
        [Route("Elements/BulkInsert/{roleId}/{screenId}")]
        public async Task<IActionResult> BulkInsertElements(int roleId, int screenId, List<RoleScreenElementDTO> dtos)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid
                || dtos == null
                || dtos.Exists(d => d.RoleId != roleId)
                )
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }

            // Check if elements in posted dto belongs to selected screen
            var list = await _screenElementsRepository.Query(x => x.ScreenId == screenId).ToListAsync();

            await using (var trans = _roleElementsRepository.GetTransaction())
            {
                try
                {
                    _roleElementsRepository.Delete(x => x.RoleId == roleId && x.ScreenElement.ScreenId == screenId);
                    foreach (RoleScreenElementDTO dto in dtos)
                    {
                        // Check if elements in posted dto belongs to selected screen
                        if (list.Exists(x => x.Id == dto.ScreenElementId))
                        {
                            _roleElementsRepository.Insert(_mapper.Map<RoleScreenElement>(dto));
                        }
                        else
                        {
                            throw new Exception(MsgKeys.InvalidInputParameters);
                        }
                    }
                    _roleElementsRepository.SaveChanges(UserName, trans);

                    // Commit transaction
                    await trans.CommitAsync();
                }
                catch (Exception e)
                {
                    // Rollback transaction
                    await trans.RollbackAsync();

                    // Show Error message
                    return BadRequest(new
                    {
                        Errors = e,
                        e.Message
                    });
                }
            }
            return Ok(MsgKeys.UpdatedSuccessfully);
        }
    }
}
