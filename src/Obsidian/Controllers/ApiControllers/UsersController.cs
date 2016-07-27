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
        public IQueryable<QueryModel.User> Get()
        {
            return _queryDbContext.Users;
        }

        [HttpPost]
        public IActionResult Post([FromBody]UserCreationDto dto)
        {
            var cmd = new CreateUserCommand(dto);
            var result = _commandBus.Send(cmd);
            return Created($"~/api/users/{result.ResultId}", null);
        }
    }
}