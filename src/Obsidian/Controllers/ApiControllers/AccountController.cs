using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace Obsidian.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {

        [Authorize(Policy = "NameIdentifierPolicy", ActiveAuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("Id")]
        public Task<ActionResult> GetUserId()
        {
            throw new NotImplementedException();
        }

    }
}