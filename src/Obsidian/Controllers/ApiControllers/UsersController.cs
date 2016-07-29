using Microsoft.AspNetCore.Mvc;
using Obsidian.Application.Commanding.ApplicationCommands;
using Obsidian.Application.Dto;
using Obsidian.Application.Commanding;
using Obsidian.QueryModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Obsidian.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IQueryModelDbContext _queryDbContext;
        private readonly CommandBus _commandBus;

        public UsersController(IQueryModelDbContext qctx, CommandBus cmdBus)
        {
            _queryDbContext = qctx;
            _commandBus = cmdBus;
        }

        [HttpGet]
        public Task<IQueryable<User>> Get() => Task.FromResult(_queryDbContext.Users);

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _queryDbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserCreationDto dto)
        {
            var cmd = new CreateUserCommand(dto);
            var result = await _commandBus.SendAsync<CreateUserCommand, Guid>(cmd);
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