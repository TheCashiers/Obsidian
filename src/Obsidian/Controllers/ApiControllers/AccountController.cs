using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Obsidian.Domain.Repositories;
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

        [Authorize(Policy = "GetUsernamePolicy", ActiveAuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("Username")]
        public async Task<ActionResult> GetUserName()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userRepository.FindByIdAsync(new Guid(userId));
            return Ok(new { Username = user.UserName });
        }

    }
}