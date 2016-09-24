using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Application.Dto;
using Obsidian.Application.ProcessManagement;
using Obsidian.Application.ScopeManagement;
using Obsidian.Domain.Repositories;
using Obsidian.Misc;
using System;
using System.Threading.Tasks;

namespace Obsidian.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class ScopesController : Controller
    {
        private readonly SagaBus _sagaBus;
        private readonly IPermissionScopeRepository _scopeRepository;

        public ScopesController(IPermissionScopeRepository scopeRepo, SagaBus bus)
        {
            _scopeRepository = scopeRepo;
            _sagaBus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = await _scopeRepository.QueryAllAsync();
            return Ok(query.ProjectTo<QueryModel.PermissionScope>());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var scope = await _scopeRepository.FindByIdAsync(id);
            if (scope == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<QueryModel.PermissionScope>(scope));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Post([FromBody]ScopeCreationDto dto)
        {
            var cmd = new CreateScopeCommand
            {
                ClaimTypes = dto.ClaimTypes,
                Description = dto.Description,
                DisplayName = dto.DisplayName,
                ScopeName = dto.ScopeName
            };

            var result = await _sagaBus.InvokeAsync<CreateScopeCommand, ScopeCreationResult>(cmd);
            if (result.Succeed)
            {
                return Created(Url.Action(nameof(GetById), new { id = result.Id }), null);
            }
            return StatusCode(412, result.Message);
        }

        [HttpPut("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Put([FromBody] UpdateScopeInfoDto dto, Guid id)
        {
            var cmd = new UpdateScopeInfoCommand
            {
                Id = id,
                Description = dto.Description,
                DisplayName = dto.DisplayName
            };
            var result = await _sagaBus.InvokeAsync<UpdateScopeInfoCommand, MessageResult<UpdateScopeInfoCommand>>(cmd);
            if (result.Succeed)
            {
                return Created(Url.Action(), null);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("{id:guid}/Claims")]
        [ValidateModel]
        public async Task<IActionResult> AddClaim(Guid id, [FromBody]string claim)
        {
            var cmd = new UpdateScopeClaimsCommand
            {
                IsAdd = true,
                Id = id,
                Claim = claim
            };
            var result = await _sagaBus.InvokeAsync<UpdateScopeClaimsCommand, MessageResult<UpdateScopeClaimsCommand>>(cmd);
            if (result.Succeed)
            {
                return Created(Url.Action(), null);
            }
            return BadRequest(result.Message);
        }
        [HttpDelete("{id:guid}/Claims/{claim}")]
        [ValidateModel]
        public async Task<IActionResult> RemoveClaim(Guid id, string claim)
        {
            var cmd = new UpdateScopeClaimsCommand
            {
                IsAdd = false,
                Id = id,
                Claim = claim
            };
            var result = await _sagaBus.InvokeAsync<UpdateScopeClaimsCommand, MessageResult<UpdateScopeClaimsCommand>>(cmd);
            if (result.Succeed)
            {
                return NoContent();
            }
            return BadRequest(result.Message);
        }
    }
}