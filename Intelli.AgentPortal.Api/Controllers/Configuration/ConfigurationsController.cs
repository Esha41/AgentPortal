using AutoMapper;
using Intelli.AgentPortal.Api.DTO;
using Intelli.AgentPortal.Api.Helpers;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.EventBus.RabbitMQ.Sender;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Intelli.AgentPortal.Api.Controllers.v1
{
    /// <summary>
    /// The Companies Controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConfigurationsController : BaseController
    {
        private readonly AgentPortalContext _context;
        private readonly IMapper _mapper;
        private readonly IEventSender _sender;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationsController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="sender">The sender.</param>
        public ConfigurationsController(AgentPortalContext context,
            IMapper mapper,
            IEventSender sender)
        {
            _context = context;
            _mapper = mapper;
            _sender = sender;
        }

        /// <summary>
        /// Gets the configurations.
        /// </summary>
        /// <returns>ConfigurationDto.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(ConfigurationHelper.Read(_context, _mapper));
        }

        /// <summary>
        /// Gets the password policy.
        /// </summary>
        /// <returns>ConfigurationDto.</returns>
        [HttpGet("password")]
        public IActionResult GetPasswordPolicy()
        {
            var dto = ConfigurationHelper.Read(_context, _mapper);
            var policy = new PasswordPolicy(dto);
            return Ok(policy);
        }

        /// <summary>
        /// Updates the configurations.
        /// </summary>
        /// <param name="dto">The ConfigurationDto.</param>
        /// <returns>ConfigurationDto.</returns>
        [HttpPut]
        public IActionResult Put(ConfigurationDto dto)
        {
            dto = ConfigurationHelper.Update(_context, _mapper, _sender, dto, UserName);
            return Ok(dto);
        }
    }
}
