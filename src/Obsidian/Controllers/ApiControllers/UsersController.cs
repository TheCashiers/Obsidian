using Microsoft.AspNetCore.Mvc;
using Obsidian.Application.Commanding.ApplicationCommands;
using Obsidian.Application.Dto;
using Obsidian.Application.Commanding;
using Obsidian.QueryModel;
using System;
using System.Linq;

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
        public IQueryable<User> Get()
        {
            return _queryDbContext.Users;
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var user = _queryDbContext.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody]UserCreationDto dto)
        {
            var cmd = new CreateUserCommand(dto);
            var resultCmd = _commandBus.Send(cmd);
            if (resultCmd.Result.Succeed)
            {
                return Created(Url.Action(nameof(GetById), new { id = resultCmd.ResultId }), null);
            }
            else
            {
                return StatusCode(412, resultCmd.Result.Exception.Message);
            }
        }
    }
}