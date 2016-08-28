using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Domain.Repositories;
using Obsidian.QueryModel.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository repo)
        {
            _clientRepository = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = await _clientRepository.QueryAllAsync();
            return Ok(query.ProjectTo<QueryModel.Client>(query));
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
    }
}
