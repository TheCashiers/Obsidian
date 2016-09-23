using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Application.Dto;
using Obsidian.Application.ProcessManagement;
using Obsidian.Application.UserManagement;
using Obsidian.Domain.Repositories;
using Obsidian.Misc;
using System;
using System.Threading.Tasks;

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

        [HttpPut("{id:guid}/Profile")]
        [ValidateModel]
        public async Task<IActionResult> UpdateProfile([FromBody]UpdateProfileDto dto)
        {
            var cmd = new EditUserProfileCommand { UserId = dto.UserId , NewProfile = dto.NewProfile };
            var result = await _sagaBus.InvokeAsync<EditUserProfileCommand, Result<EditUserProfileCommand>>(cmd);
            if(result.Succeed)
            {
                return Ok(Url.Action(nameof(UpdateProfile)));
            }
            //if user doesn't exist.
            return BadRequest(result.Message);
        }

        [HttpPut("{id:guid}/Password")]
        [ValidateModel]
        public async Task<IActionResult> SetPassword([FromBody]UserPasswordSettingDto dto)
        {
            var cmd = new SetUserPasswordCommand {  NewPassword = dto.NewPassword, UserId = dto.UserId };
            var result = await _sagaBus.InvokeAsync<SetUserPasswordCommand, Result<SetUserPasswordCommand>>(cmd);
            if (result.Succeed)
            {
                return Ok(Url.Action(nameof(SetPassword)));
            }
            //if user doesn't exist.
            return BadRequest(result.Message);
        }
    }
}