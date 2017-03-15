using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Obsidian.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using Obsidian.Domain;
using Obsidian.Misc;
namespace Obsidian.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {

        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository repo)
        {
            _userRepository = repo;
        }
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        [RequireClaim(ClaimTypes.NameIdentifier,null)]
        [HttpGet]
        [Route("Profile")]
        public async Task<ActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userRepository.FindByIdAsync(new Guid(userId));
            var dic = new Dictionary<string, string>();
            var claims = user.GetClaims(User.Claims.Select(c=>c.Type)).ToDictionary(cm=>cm.Type,cn=>cn.Value);
            return Ok(claims);
        }
    }
}