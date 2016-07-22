using Microsoft.AspNetCore.Mvc;
using Obsidian.QueryModel;
using System.Linq;

namespace Obsidian.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IQueryModelDbContext _queryDbContext;

        public UsersController(IQueryModelDbContext qctx)
        {
            _queryDbContext = qctx;
        }

        [HttpGet]
        public IQueryable<QueryModel.User> Get()
        {
            return _queryDbContext.Users;
        }
    }
}