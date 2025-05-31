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
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Controllers.v1
{
    /// <summary>
    /// The Countries Controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountriesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICustomRepository<Country> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountriesController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="AgentPortalContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        public CountriesController(AgentPortalContext context,
            IMapper mapper,
            IEventSender sender)
        {
            _repository = new CustomRepository<Country>(context);

            ((CustomRepository<Country>)_repository).AfterSave = (logs) =>
                 sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

            _mapper = mapper;
        }

        /// <summary>
        /// Get All Companies.
        /// </summary>
        /// <returns>List of CompanyDTO</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var dto = new CountryDTO();
            var result = await _repository.GetAllActiveAsync(nameof(dto.Name));
            return Ok(new { Items = result.List.Select(x => _mapper.Map<CountryDTO>(x)).ToList() });
        }
    }
}
