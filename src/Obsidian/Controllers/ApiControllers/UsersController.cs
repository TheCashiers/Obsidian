using Microsoft.AspNetCore.Mvc;
using Obsidian.Application.Dto;
using Obsidian.Application.ProcessManagement;
using Obsidian.Application.UserManagement;
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
        private readonly SagaBus _sagaBus;
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepo, SagaBus bus)
        {
            _userRepository = userRepo;
            _sagaBus = bus;
        }

        [HttpGet]
        public async Task<IQueryable<User>> Get() => from u in await _userRepository.QueryAllAsync()
                                                     select new User
                                                     {
                                                         Id = u.Id,
                                                         DisplayName = $"{u.Profile.GivenName} {u.Profile.SurnName}",
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
            var cmd = new CreateUserCommand { UserName = dto.UserName, Password = dto.Password };
            var result = await _sagaBus.InvokeAsync<CreateUserCommand, UserCreationResult>(cmd);
            if (result.Succeed)
            {
                return Created(Url.Action(nameof(GetById), new { id = result.UserId }), null);
            }
            // currently, the only reason for failure is that a user of the same username exists.
            return StatusCode(412, result.Message);
        }
    }
}