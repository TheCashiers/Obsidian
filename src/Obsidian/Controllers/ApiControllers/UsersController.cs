using Microsoft.AspNetCore.Mvc;
using Obsidian.Application.Dto;
using Obsidian.Application.OldCommanding;
using Obsidian.Application.OldCommanding.ApplicationCommands;
using Obsidian.Domain.Repositories;
using Obsidian.QueryModel;
using Obsidian.QueryModel.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly OldCommandBus _commandBus;
        private readonly IUserRepository _userRepository;

        public UsersController(OldCommandBus cmdBus, IUserRepository userRepo)
        {
            _commandBus = cmdBus;
            _userRepository = userRepo;
        }

        [HttpGet]
        public async Task<IQueryable<User>> Get() => from u in await _userRepository.QueryAllAsync()
                                                     select new User
                                                     {
                                                         Id = u.Id,
                                                         DisplayName = u.Profile.DisplayName,
                                                         Gender = (Gender)u.Profile.Gender,
                                                         UserName = u.UserName
                                                     };

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userRepository.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserCreationDto dto)
        {
            var cmd = new CreateUserOldCommand(dto);
            var result = await _commandBus.SendAsync<CreateUserOldCommand, Guid>(cmd);
            if (result.Succeed)
            {
                return Created(Url.Action(nameof(GetById), new { id = result.Data }), null);
            }
            else if (result.Exception is InvalidOperationException)
            {
                // currently, the only reason for InvalidOperationException is that a user of the same username exists.
                return StatusCode(412, result.Exception.Message);
            }
            return BadRequest();
        }
    }
}