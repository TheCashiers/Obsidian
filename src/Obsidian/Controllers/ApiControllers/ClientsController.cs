using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Application.ClientManagement;
using Obsidian.Application.Dto;
using Obsidian.Application.ProcessManagement;
using Obsidian.Domain.Repositories;
using Obsidian.Misc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.SwaggerGen;

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
        public async Task<IActionResult> GetById(Guid id)
        {
            var client = await _clientRepository.FindByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<QueryModel.Client>(client));
        }

        /// <summary>
        /// Create a Client
        /// </summary>
        /// <param name="dto">A DTO containing informations for creating a Client</param>
        [HttpPost]
        [ValidateModel]
        [SwaggerResponse(201, typeof(CreatedResult))]
        [SwaggerResponse(400), SwaggerResponse(412)]
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