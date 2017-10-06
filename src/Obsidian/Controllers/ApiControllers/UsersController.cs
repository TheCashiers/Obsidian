using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Application;
using Obsidian.Application.Dto;
using Obsidian.Application.UserManagement;
using Obsidian.Authorization;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Foundation.ProcessManagement;
using Obsidian.Misc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly SagaBus _sagaBus;
        private readonly IUserRepository _userRepository;
        private readonly UserManagementService _userManagementService;

        public UsersController(IUserRepository userRepo, SagaBus bus, UserManagementService userManagementService)
        {
            _userRepository = userRepo;
            _sagaBus = bus;
            _userManagementService = userManagementService;
        }

        [HttpGet]
        [RequireClaim(ManagementAPIClaimsType.IsUserAcquirer, "Yes")]
        public async Task<IActionResult> Get()
        {
            var query = await _userRepository.QueryAllAsync();
            return Ok(query.ProjectTo<QueryModel.User>());
        }

        [HttpGet("{id:guid}")]
        [RequireClaim(ManagementAPIClaimsType.IsUserAcquirer, "Yes")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userRepository.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<QueryModel.User>(user));
        }

        [HttpPost]
        [ValidateModel]
        [RequireClaim(ManagementAPIClaimsType.IsUserCreator, "Yes")]
        public async Task<IActionResult> Post([FromBody]UserCreationDto dto)
        {
            try
            {
                var newUser = await _userManagementService.CreateAsync(dto);
                return Created(Url.Action(nameof(GetById), new { id = newUser.Id }), null);
            }
            catch (ArgumentException ex)
            {
                // HTTP 409 Conflict
                return StatusCode(409, ex.Message);
            }
        }

        [HttpPut("{id:guid}/Claims")]
        [ValidateModel]
        [RequireClaim(ManagementAPIClaimsType.IsUserClaimsEditor, "Yes")]
        public async Task<IActionResult> UpdateClaims([FromBody]UpdateUserClaimsDto dto, Guid id)
        {
            var cmd = new UpdateUserClaimCommand { UserId = id, Claims = dto.Claims.ToDictionary(t => t.ClaimType, v => v.ClaimValue) };
            var result = await _sagaBus.InvokeAsync<UpdateUserClaimCommand, MessageResult>(cmd);
            if (result.Succeed)
            {
                return Created(Url.Action(), null);
            }
            //if user doesn't exist.
            return BadRequest(result.Message);
        }

        [HttpPut("{id:guid}/Profile")]
        [RequireClaim(ManagementAPIClaimsType.IsUserProfileEditor, "Yes")]
        [ValidateModel]
        public async Task<IActionResult> UpdateProfile([FromBody]UserProfile profile, Guid id)
        {
            var cmd = new UpdateUserProfileCommand { UserId = id, NewProfile = profile };
            var result = await _sagaBus.InvokeAsync<UpdateUserProfileCommand, MessageResult>(cmd);
            if (result.Succeed)
            {
                return Created(Url.Action(), null);
            }
            //if user doesn't exist.
            return BadRequest(result.Message);
        }

        [HttpPut("{id:guid}/Password")]
        [ValidateModel]
        [RequireClaim(ManagementAPIClaimsType.IsUserPasswordEditor, "Yes")]
        public async Task<IActionResult> SetPassword([FromBody]UpdateUserPasswordDto dto, Guid id)
        {
            var cmd = new UpdateUserPasswordCommand { NewPassword = dto.Password, UserId = id };
            var result = await _sagaBus.InvokeAsync<UpdateUserPasswordCommand, MessageResult>(cmd);
            if (result.Succeed)
            {
                return Created(Url.Action(), null);
            }
            //if user doesn't exist.
            return BadRequest(result.Message);
        }

        [HttpPut("{id:guid}/UserName")]
        [ValidateModel]
        [RequireClaim(ManagementAPIClaimsType.IsUserNameEditor, "Yes")]
        public async Task<IActionResult> SetUserName([FromBody]UpdateUserNameDto dto, Guid id)
        {
            var cmd = new UpdateUserNameCommand
            {
                UserId = id,
                UserName = dto.UserName
            };
            var result = await _sagaBus.InvokeAsync<UpdateUserNameCommand, MessageResult>(cmd);
            if (result.Succeed)
            {
                return Created(Url.Action(), null);
            }
            //if error
            return BadRequest(result.Message);
        }
    }
}