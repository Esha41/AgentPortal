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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Controllers.v1
{
    /// <summary>
    /// The Companies Controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CompaniesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IRepository<Company> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompaniesController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="AgentPortalContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        public CompaniesController(AgentPortalContext context,
            IMapper mapper,
            ILogger<CompaniesController> logger,
            IEventSender sender)
        {
            _repository = new GenericRepository<Company>(context);

            ((GenericRepository<Company>)_repository).AfterSave = (logs) =>
                 sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Companies.
        /// </summary>
        /// <returns>List of CompanyDTO</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var dto = new CompanyDTO();
            var result = await _repository.GetAllActiveAsync(nameof(dto.Name));
            result = FilterCompaniesBasedUponCompanyIds(result);
            return Ok(new { Items = result.List.Select(x => _mapper.Map<CompanyDTO>(x)).ToList() });
        }

        /// <summary>
        /// Gets the companies w.r.t quey string parameters.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] QueryStringParams queryStringParams)
        {
            _logger.LogInformation("Get Companies Called with params: {0}", queryStringParams);
            queryStringParams.SetFilterExpression(
                   queryStringParams.FilterExpression.Replace("CompanyName", "Name"));
            queryStringParams.SetOrderBy(queryStringParams.OrderBy.Replace("CompanyName", "Name"));

            PagedResult<CompanyDTO> result = null;
            try
            {
                QueryResult<Company> queryResult = new();
                if (!queryStringParams.IsExport)
                {
                    queryResult = _repository.Get(
                                    queryStringParams.FilterExpression,
                                    queryStringParams.OrderBy,
                                    queryStringParams.PageSize,
                                    queryStringParams.PageNumber);
                }
                else
                {
                    queryResult = _repository.Get(
                                    queryStringParams.FilterExpression,
                                    queryStringParams.OrderBy);
                }

                queryResult = FilterCompaniesBasedUponCompanyIds(queryResult);

                int total = queryResult.Count;
                IEnumerable<Company> roleList = queryResult.List;

                result = new PagedResult<CompanyDTO>(
                                total,
                                queryStringParams.PageNumber,
                                roleList.Select(x => _mapper.Map<CompanyDTO>(x)).ToList(),
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
        /// Creates the company.
        /// </summary>
        /// <param name="dto">The company dto.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPost]
        public IActionResult Post(CompanyDTO dto)
        {
            try
            {
                // Checking if the passed DTO is valid
                if (!ModelState.IsValid || dto == null)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                // Checking if there is duplicate code
                if (_repository.Query(s=>s.Code==dto.Code).FirstOrDefault()!=null)
                    return BadRequest(MsgKeys.CodeDuplicated);

                var getCompanies = _repository.Get().List;
                var incremnetedId = getCompanies.LastOrDefault().Id;

                var entity = _mapper.Map<Company>(dto);
                entity.Id = ++incremnetedId;
                _repository.Insert(entity);
                _repository.SaveChanges(UserName, null);

                return Ok(_mapper.Map<CompanyDTO>(entity));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the company.
        /// </summary>
        /// <param name="id">The company id.</param>
        /// <returns>The CompanyDTO.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = _repository.Query(x => x.Id == id)
                        .Include(x => x.DocumentsPerCompany)
                        .Include(x => x.UserCompanies)
                        .ThenInclude(x=>x.SystemUser);


            var entity = await query.FirstOrDefaultAsync();


            if (entity == null) return NotFound();



            var dto = _mapper.Map<CompanyDTO>(entity);

            dto.SystemUsers = GetAllUsersOfSelectedCompany(entity);
            return Ok(dto);
        }

        /// <summary>
        /// Updates the company.
        /// </summary>
        /// <param name="id">The company id.</param>
        /// <param name="dto">The CompanyDTO.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, CompanyDTO dto)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null || id != dto.Id)
                return BadRequest(MsgKeys.InvalidInputParameters);

            var entity = _mapper.Map<Company>(dto);

            _repository.Update(entity);
            _repository.SaveChanges(UserName, null);

            return Ok(_mapper.Map<CompanyDTO>(entity));
        }

        /// <summary>
        /// Delete the company.
        /// </summary>
        /// <param name="id">The company id.</param>
        /// <returns>An IActionResult.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
                var query = _repository.Query(x => x.Id == id)
                            .Include(x => x.DocumentsPerCompany)
                            .Include(x => x.UserCompanies).ToList();

                var entity =  query.FirstOrDefault();
                if (entity == null) return NotFound();
                entity.IsActive = false;
                _repository.SaveChanges(UserName, null);

                return Ok(MsgKeys.DeletedSuccessfully);

              /*  if (entity.DocumentsPerCompany.Count == 0 && entity.UserCompanies.Count == 0)
                {
                    entity.IsActive = false;
                    _repository.SaveChanges(UserName, null);

                    return Ok(MsgKeys.DeletedSuccessfully);
                }

                return BadRequest(MsgKeys.ChildEntityExists);*/

        }


        private QueryResult<Company> FilterCompaniesBasedUponCompanyIds(QueryResult<Company> result)
        {
            // if user is associated it one or more companies then filter all those companies
            if(CompanyIds != null && CompanyIds.Count >= 1)
            {
                result.List =  result.List.FindAll(x => CompanyIds.Contains(x.Id));
                result.Count = result.List.Count;
            }

            return result;
        }

        private List<UserReadDTO> GetAllUsersOfSelectedCompany(Company entity)
        {
            List<UserReadDTO> users = new();
            //add company users in dto
            if (entity != null)
            {
                foreach (var userCompany in entity.UserCompanies)
                {
                    List<string> userCompanies = GetAllAssociatedCompanies(userCompany.SystemUser.UserCompanies);
                    userCompany.SystemUser.UserCompanies = null;
                    var dto = _mapper.Map<UserReadDTO>(userCompany.SystemUser);
                    dto.CompanyNames = userCompanies;
                    users.Add(dto);

                }
            }

            return users;
        }


        private List<string> GetAllAssociatedCompanies(ICollection<UserCompany> userCompany)
        {
            List<string> userCompanies = new();

            if(userCompany != null)
            {
                foreach (var company in userCompany)
                {
                    if(company.Company != null)
                    {
                        userCompanies.Add(company.Company.Name);
                    }

                }
            }


            return userCompanies;

        }
    }
}
