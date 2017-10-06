using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Application;
using Obsidian.Application.ClientManagement;
using Obsidian.Application.Dto;
using Obsidian.Authorization;
using Obsidian.Domain.Repositories;
using Obsidian.Foundation.ProcessManagement;
using Obsidian.Misc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        private readonly SagaBus _sagaBus;
        private readonly IClientRepository _clientRepository;
        private readonly ClientService _service;

        public ClientsController(IClientRepository repo, SagaBus bus, ClientService service)
        {
            _clientRepository = repo;
            _sagaBus = bus;
            _service = service;
        }

        /// <summary>
        /// Get all Clients
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, typeof(IQueryable<QueryModel.Client>))]
        [RequireClaim(ManagementAPIClaimsType.IsClientAcquirer, "Yes")]
        public async Task<IActionResult> Get()
        {
            var query = await _clientRepository.QueryAllAsync();
            return Ok(query.AsQueryable().ProjectTo<QueryModel.Client>(query));
        }

        /// <summary>
        /// Get a Client by Id
        /// </summary>
        /// <param name="id">The Id of the Client</param>
        [HttpGet("{id:guid}")]
        [SwaggerResponse(200, typeof(QueryModel.Client))]
        [SwaggerResponse(404)]
        [RequireClaim(ManagementAPIClaimsType.IsClientAcquirer, "Yes")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var client = await _clientRepository.FindByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<QueryModel.Client>(client));
        }

        [HttpGet("{id:guid}/Secret")]
        [RequireClaim(ManagementAPIClaimsType.IsClientSecretAcquirer, "Yes")]
        public async Task<IActionResult> GetSecretById(Guid id)
        {
            var client = await _clientRepository.FindByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(new { Secret = client.Secret });
        }

        /// <summary>
        /// Create a Client
        /// </summary>
        /// <param name="dto">A DTO containing informations for creating a Client</param>
        [HttpPost]
        [ValidateModel]
        [SwaggerResponse(201, typeof(CreatedResult))]
        [SwaggerResponse(400), SwaggerResponse(412)]
        [RequireClaim(ManagementAPIClaimsType.IsClientCreator, "Yes")]
        public async Task<IActionResult> Post([FromBody] ClientCreationDto dto)
        {
            var client = await _service.CreateClient(dto.DisplayName, dto.RedirectUri);
            if (client != null)
            {
                var url = Url.Action(nameof(GetById), new { id = client.Id });
                return Created(url, null);
            }
            return StatusCode(412);
        }

        [HttpPut("{id:guid}")]
        [RequireClaim(ManagementAPIClaimsType.IsClientEditor, "Yes")]
        [ValidateModel]
        public async Task<IActionResult> Put([FromBody]UpdateClientDto dto, Guid id)
        {
            var client = await _clientRepository.FindByIdAsync(id);
            if (client == null) return NotFound();
            client = await _service.UpdateClient(id, dto.DisplayName, dto.RedirectUri);
            if (client != null)
            {
                return Created(Url.Action(), null);
            }
            return BadRequest();
        }

        [HttpPut("{id:guid}/Secret")]
        [RequireClaim(ManagementAPIClaimsType.IsClientSecretEditor, "Yes")]
        [ValidateModel]
        public async Task<IActionResult> UpdateSecret(Guid id)
        {
            var client = await _clientRepository.FindByIdAsync(id);
            if (client == null) return NotFound();
            client = await _service.UpdateClientSecret(id);
            if (client != null)
            {
                return Created(Url.Action(), null);
            }
            return BadRequest();
        }
    }
}