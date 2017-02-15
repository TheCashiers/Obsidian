using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Application.Dto;
using Obsidian.Application.ProcessManagement;
using Obsidian.Application.UserManagement;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Misc;
using System;
using System.Threading.Tasks;
using System.Linq;
namespace Obsidian.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly SagaBus _sagaBus;
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepo, SagaBus bus)
        {
            _userRepository = userRepo;
            _sagaBus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = await _userRepository.QueryAllAsync();
            return Ok(query.ProjectTo<QueryModel.User>());
        }

        [HttpGet("{id:guid}")]
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
        public async Task<IActionResult> Post([FromBody]UserCreationDto dto)
        {
            var cmd = new CreateUserCommand { UserName = dto.UserName, Password = dto.Password };
            var result = await _sagaBus.InvokeAsync<CreateUserCommand, UserCreationResult>(cmd);
            if (result.Succeed)
            {
                return Created(Url.Action(nameof(GetById), new { id = result.UserId }), null);
            }
            // currently, the only reason for failure is that a user of the same username exists.
            return StatusCode(412, result.Message);
        }

        [HttpPut("{id:guid}/Claims")]
        [ValidateModel]
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