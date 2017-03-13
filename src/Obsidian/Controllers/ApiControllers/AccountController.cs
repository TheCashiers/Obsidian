using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace Obsidian.Controllers.ApiControllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {

        [Authorize(Policy = "NameIdentifierPolicy")]
        [HttpGet]
        [Route("Id")]
        public Task<ActionResult> GetUserId()
        {
            throw new NotImplementedException();
        }

    }
}