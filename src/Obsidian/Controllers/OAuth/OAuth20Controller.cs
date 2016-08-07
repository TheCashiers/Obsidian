using Microsoft.AspNetCore.Mvc;
using Obsidian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//TODO: remove this when implemented
#pragma warning disable CS1998

namespace Obsidian.Controllers.OAuth
{
    public class OAuth20Controller : Controller
    {
        [Route("oauth20/authorize")]
        [HttpGet]
        public async Task<IActionResult> Authorize([FromQuery]AuthorizationRequestModel model)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return View("SignIn");
            }
            //TODO: if user did not authorized this app before, show permissons page
            //TODO: vaildate client
            return StatusCode(501);
        }

        [Route("oauth20/authorize")]
        [HttpPost]
        public async Task<IActionResult> Authorize([FromBody]OAuthSignInModel model)
        {
            //TODO: sign user in
            //TODO: if user did not authorized this app before, show permissons page
            //TODO: vaildate client
            return StatusCode(501);
        }

        [Route("oauth20/token")]
        [HttpPost]
        public async Task<IActionResult> Token()
        {
            return StatusCode(501);
        }
    }
}
