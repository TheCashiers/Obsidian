using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Application;
using Obsidian.Application.Dto;
using Obsidian.Application.ScopeManagement;
using Obsidian.Authorization;
using Obsidian.Domain.Repositories;
using Obsidian.Foundation;
using Obsidian.Misc;
using System;
using System.Threading.Tasks;

namespace Obsidian.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class ScopesController : Controller
    {
        private readonly IPermissionScopeRepository _scopeRepository;
        private readonly ScopeManagementService _service;

        public ScopesController(IPermissionScopeRepository scopeRepo, ScopeManagementService service)
        {
            _scopeRepository = scopeRepo;
            _service = service;
        }

        [HttpGet]
        [RequireClaim(ManagementAPIClaimsType.IsScopeAcquirer, "Yes")]
        public async Task<IActionResult> Get()
        {
            var query = await _scopeRepository.QueryAllAsync();
            return Ok(query.ProjectTo<QueryModel.PermissionScope>());
        }

        [HttpGet("{id:guid}")]
        [RequireClaim(ManagementAPIClaimsType.IsScopeAcquirer, "Yes")]
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
        [RequireClaim(ManagementAPIClaimsType.IsScopeCreator, "Yes")]
        public async Task<IActionResult> Post([FromBody]ScopeCreationDto dto)
        {
            var scope = await _service.CreateScope(dto.ScopeName, dto.DisplayName, dto.Description, dto.Claims);
            if (scope != null)
            {
                return Created(Url.Action(nameof(GetById), new { id = scope.Id }), null);
            }
            return StatusCode(412);
        }

        [HttpPut("{id:guid}")]
        [ValidateModel]
        [RequireClaim(ManagementAPIClaimsType.IsScopeEditor, "Yes")]
        public async Task<IActionResult> Put([FromBody] UpdateScopeDto dto, Guid id)
        {
            try
            {
                await _service.UpdateScope(id, dto.DisplayName, dto.Description, dto.Claims);
                return Created(Url.Action(), null);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }         
        }
    }
}