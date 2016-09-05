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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = await _clientRepository.QueryAllAsync();
            return Ok(query.ToList().AsQueryable().ProjectTo<QueryModel.Client>(query));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var client = await _clientRepository.FindByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<QueryModel.Client>(client));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Post([FromBody] ClientCreationDto dto)
        {
            var cmd = new CreateClientCommand { DisplayName = dto.DisplayName, RedirectUri = dto.RedirectUri };
            var result = await _sagaBus.InvokeAsync<CreateClientCommand, ClientCreationResult>(cmd);
            if (result.Succeed)
            {
                return Created(Url.Action(nameof(GetById), new { id = result.Id }), null);
            }
            return StatusCode(412, result.Message);
        }
    }
}