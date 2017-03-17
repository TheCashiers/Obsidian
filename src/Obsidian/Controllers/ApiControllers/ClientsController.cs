using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Application;
using Obsidian.Application.ClientManagement;
using Obsidian.Application.Dto;
using Obsidian.Application.ProcessManagement;
using Obsidian.Authorization;
using Obsidian.Domain.Repositories;
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

        public ClientsController(IClientRepository repo, SagaBus bus)
        {
            _clientRepository = repo;
            _sagaBus = bus;
        }

        /// <summary>
        /// Get all Clients
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, typeof(IQueryable<QueryModel.Client>))]
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        [RequireClaim(ManagementAPIClaimsType.Client, "Get")]
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
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        [RequireClaim(ManagementAPIClaimsType.Client, "Get")]
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
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        [RequireClaim(ManagementAPIClaimsType.ClientSecret, "Get")]
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
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        [RequireClaim(ManagementAPIClaimsType.Client, "Add")]
        public async Task<IActionResult> Post([FromBody] ClientCreationDto dto)
        {
            var cmd = new CreateClientCommand { DisplayName = dto.DisplayName, RedirectUri = dto.RedirectUri };
            var result = await _sagaBus.InvokeAsync<CreateClientCommand, ClientCreationResult>(cmd);
            if (result.Succeed)
            {
                var url = Url.Action(nameof(GetById), new { id = result.Id });
                return Created(url, null);
            }
            return StatusCode(412, result.Message);
        }

        [HttpPut("{id:guid}")]
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        [RequireClaim(ManagementAPIClaimsType.Client, "Update")]
        [ValidateModel]
        public async Task<IActionResult> Put([FromBody]UpdateClientDto dto, Guid id)
        {
            var cmd = new UpdateClientCommand { ClientId = id, DisplayName = dto.DisplayName, RedirectUri = dto.RedirectUri };
            var result = await _sagaBus.InvokeAsync<UpdateClientCommand, MessageResult>(cmd);
            if (result.Succeed)
            {
                return Created(Url.Action(), null);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id:guid}/Secret")]
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        [RequireClaim(ManagementAPIClaimsType.ClientSecret, "Update")]
        [ValidateModel]
        public async Task<IActionResult> UpdateSecret(Guid id)
        {
            var cmd = new UpdateClientSecretCommand { ClientId = id };
            var result = await _sagaBus.InvokeAsync<UpdateClientSecretCommand, ClientSecretUpdateResult>(cmd);
            if (result.Succeed)
            {
                return Created(Url.Action(), null);
            }
            return BadRequest(result.Message);
        }
    }
}