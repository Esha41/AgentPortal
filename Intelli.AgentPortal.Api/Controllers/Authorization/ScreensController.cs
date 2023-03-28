using AutoMapper;
using Intelli.AgentPortal.Api.DTO;
using Intelli.AgentPortal.Domain.Core.Helpers;
using Intelli.AgentPortal.Domain.Core.Repository;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.EventBus.RabbitMQ.Event;
using Intelli.AgentPortal.EventBus.RabbitMQ.Sender;
using Intelli.AgentPortal.Shared;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Controllers.v1
{
    /// <summary>
    /// The Screens Controller.
    /// This controller is responsible for providing list of screens and their related screen elements.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ScreensController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IRepository<Screen> _repository;
        private readonly IRepository<ScreenElement> _repositoryForElements;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreensController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="AgentPortalContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        public ScreensController(AgentPortalContext context,
            IMapper mapper,
            ILogger<ScreensController> logger,
            IEventSender sender)
        {
            _repository = new GenericRepository<Screen>(context);
            _repositoryForElements = new GenericRepository<ScreenElement>(context);

            ((GenericRepository<Screen>)_repository).AfterSave =
            ((GenericRepository<ScreenElement>)_repositoryForElements).AfterSave = (logs) =>
                sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Screens.
        /// </summary>
        /// <param name="queryStringParams">Optional QueryStringParams (from request query string) for filtering, ordering and paging of data</param>
        /// <returns>ActionResult of ScreensDTO list in JSON format</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ScreensDTO>> GetScreens([FromQuery] QueryStringParams queryStringParams)
        {
            PagedResult<ScreensDTO> result = null;
            try
            {
                QueryResult<Screen> queryResult = _repository.Get(
                        queryStringParams.FilterExpression,
                        queryStringParams.OrderBy,
                        queryStringParams.PageSize,
                        queryStringParams.PageNumber);

                int total = queryResult.Count;
                IEnumerable<Screen> list = queryResult.List;

                result = new PagedResult<ScreensDTO>(
                        total,
                        queryStringParams.PageNumber,
                        list.Select(x => _mapper.Map<ScreensDTO>(x)).ToList(),
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
        /// Get All Screens.
        /// </summary>
        /// <returns>ActionResult of ScreensDTO list in JSON format</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllScreens()
        {
            var dto = new ScreensDTO();
            var result = await _repository.GetAllActiveAsync(nameof(dto.ScreenName));
            return Ok(new { Items = result.List.Select(x => _mapper.Map<ScreensDTO>(x)).ToList() });
        }

        /// <summary>
        /// Get Screen by its Id.
        /// </summary>
        /// <param name="id">Unique Id of the required Screen</param>
        /// <returns>ActionResult of ScreensDTO in JSON format</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ScreensDTO>> GetScreen(int id)
        {
            Screen objFromDB = await _repository.GetById(id);

            if (objFromDB == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ScreensDTO>(objFromDB));
        }

        /// <summary>
        /// Get List of Screen Elements by its ScreenId.
        /// </summary>
        /// <param name="screenId">Unique Id of the Screen Model</param>
        /// <returns>ActionResult of ScreenElementDTO in JSON format</returns>
        [HttpGet]
        [Route("Elements/{screenId}")]
        public ActionResult<IEnumerable<ScreenElementDTO>> GetElementsByScreenId(int screenId)
        {
            var list = _repositoryForElements.Query()
                .Where(q => q.ScreenId == screenId)
                .OrderBy(x => x.ScreenElementName)
                .ToList();

            return Ok(new { Items = list.Select(x => _mapper.Map<ScreenElementDTO>(x)).ToList() });
        }

        /// <summary>
        /// Posts the screen.
        /// </summary>
        /// <param name="dto">The screens dto.</param>
        /// <returns>An ActionResult of screen dto.</returns>
        [HttpPost]
        public ActionResult<ScreensDTO> PostScreen(ScreensDTO dto)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }

            Screen scr = _mapper.Map<Screen>(dto);

            _repository.Insert(scr);
            _repository.SaveChanges(UserName, null);

            _logger.LogInformation("Screen inserted with id: {0}", scr.Id);

            return Ok(_mapper.Map<ScreensDTO>(scr));
        }

        /// <summary>
        /// Puts the screen.
        /// </summary>
        /// <param name="id">id of screen</param>
        /// <param name="dto">The screens dto.</param>
        /// <returns>An ActionResult of screen dto.</returns>
        [HttpPut("{id}")]
        public ActionResult<ScreensDTO> PutScreen(int id, ScreensDTO dto)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null || id != dto.Id)
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }

            var entity = _mapper.Map<Screen>(dto);
            _repository.Update(entity);
            _repository.SaveChanges(UserName, null);

            _logger.LogInformation("Screen updated with id: {0}", entity.Id);

            return Ok(dto);
        }

        /// <summary>
        /// Posts the screens.
        /// </summary>
        /// <param name="screenNames">The list of screen names.</param>
        /// <returns>An ActionResult of screen dtos list.</returns>
        [HttpPost]
        [Route("BulkInsert")]
        public ActionResult<Response> PostAll(List<string> screenNames)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || screenNames == null)
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }

            foreach (var name in screenNames)
            {
                _repository.Insert(new Screen { ScreenName = name });
            }
            _repository.SaveChanges(UserName, null);

            return Ok(MsgKeys.CreatedSuccessfully);
        }
    }
}
