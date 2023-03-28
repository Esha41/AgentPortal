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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Controllers.v1
{
    /// <summary>
    /// The screen elements controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ScreenElementsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        private readonly IRepository<ScreenElement> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenElementsController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="sender">The sender.</param>
        public ScreenElementsController(AgentPortalContext context, IMapper mapper,
            ILogger<ScreenElementsController> logger,
            IEventSender sender)
        {
            _repository = new GenericRepository<ScreenElement>(context);

            ((GenericRepository<ScreenElement>)_repository).AfterSave = (logs) =>
                sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all the screen elements.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllScreenElements()
        {
            var dto = new ScreenElementDTO();
            var result = await _repository.GetAllActiveAsync(nameof(dto.ScreenElementName));
            return Ok(new { Items = result.List.Select(x => _mapper.Map<ScreenElementDTO>(x)).ToList() });
        }

        /// <summary>
        /// Gets the screen element.
        /// </summary>
        /// <param name="id">The id of the screen element.</param>
        /// <returns>A Task.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ScreenElementDTO>> GetScreenElement(int id)
        {
            var objFromDB = await _repository.GetById(id);

            if (objFromDB == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ScreenElementDTO>(objFromDB));
        }

        /// <summary>
        /// Posts the screen element.
        /// </summary>
        /// <param name="dto">The dto of the screen element.</param>
        /// <returns>An ActionResult.</returns>
        [HttpPost]
        public ActionResult<ScreenElementDTO> PostScreenElement(ScreenElementDTO dto)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }

            var entity = _mapper.Map<ScreenElement>(dto);

            _repository.Insert(entity);
            _repository.SaveChanges(UserName, null);

            _logger.LogInformation("Screen Element inserted with id: {0}", entity.Id);

            return Ok(_mapper.Map<ScreenElementDTO>(entity));
        }

        /// <summary>
        /// Updates the screen element.
        /// </summary>
        /// <param name="id">The id of the screen element to be updated.</param>
        /// <param name="dto">The screen element dto.</param>
        /// <returns>An ActionResult.</returns>
        [HttpPut("{id}")]
        public ActionResult<ScreenElementDTO> PutScreenElement(int id, ScreenElementDTO dto)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null || id != dto.Id)
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }

            var entity = _mapper.Map<ScreenElement>(dto);
            _repository.Update(entity);
            _repository.SaveChanges(UserName, null);

            _logger.LogInformation("Screen Element updated with id: {0}", entity.Id);

            return Ok(dto);
        }

        /// <summary>
        /// Posts all the screen elements for a particular screen.
        /// </summary>
        /// <param name="screenId">The screen id.</param>
        /// <param name="screenElementNames">The screen element names array.</param>
        /// <returns>An ActionResult.</returns>
        [HttpPost]
        [Route("BulkInsert/{screenId}")]
        public ActionResult<Response> PostAll(int screenId, List<string> screenElementNames)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || screenElementNames == null)
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }

            foreach (var name in screenElementNames)
            {
                _repository.Insert(new ScreenElement { ScreenId = screenId, ScreenElementName = name });
            }
            _repository.SaveChanges(UserName, null);

            return Ok(MsgKeys.CreatedSuccessfully);
        }
    }
}
