using AutoMapper;
using Intelli.AgentPortal.Api.DTO;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Domain.Repository;
using Intelli.AgentPortal.Domain.Repository.Impl;
using Intelli.AgentPortal.EventBus.RabbitMQ.Sender;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Controllers.Batches
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ResellersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICompanyRepository<AspNetReseller> _repositoryResellers;
        /// <summary>
        /// Initializes a new instance of the <see cref="BatchController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="sender">The Event Sender.</param>
        public ResellersController(AgentPortalContext context, IMapper mapper,
            ILogger<ResellersController> logger,
            IEventSender sender)
        {
            _repositoryResellers = new CompanyEntityRepository<AspNetReseller>(context);

            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("allResellers")]
        public async Task<IActionResult> GetAllResellers()
        {
            try
            {
                var query =  _repositoryResellers.Get(CompanyIds);
                var resllerList = query.List;
                return Ok(new { items = resllerList.Select(x => _mapper.Map<ResellersDTO>(x)) });
            }
            catch (Exception e)
            {

            }
            return Ok();
        }
    }
}
