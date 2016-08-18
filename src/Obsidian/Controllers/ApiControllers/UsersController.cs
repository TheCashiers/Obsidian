using Microsoft.AspNetCore.Mvc;
using Obsidian.Application.OldCommanding.ApplicationCommands;
using Obsidian.Application.Dto;
using Obsidian.Application.OldCommanding;
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
        private readonly OldCommandBus _commandBus;

        public UsersController(IQueryModelDbContext qctx, OldCommandBus cmdBus)
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