using Microsoft.AspNetCore.Mvc;
using Obsidian.Application;
using Obsidian.Application.Services;
using Obsidian.Application.Dto;
using Obsidian.Authorization;
using Obsidian.Domain.Repositories;
using System.Threading.Tasks;
using Obsidian.Misc;

namespace Obsidian.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityService _identityService;

        public AccountController(IUserRepository repo, IIdentityService identityService)
        {
            _userRepository = repo;
            _identityService = identityService;
        }

        [RequireClaim(AccountAPIClaimTypes.Profile, "Get")]
        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _identityService.GetCurrentUserAsync();
            return Ok(user.Profile);
        }

        [RequireClaim(AccountAPIClaimTypes.Password,"Update")]
        [ValidateModel]
        [HttpPut("Password")]
        public async Task<IActionResult> SetPassword([FromBody]UpdateAccountPasswordDto dto)
        {
            await _identityService.SetCurrentUserPasswordAsync(dto.OldPassword, dto.NewPassword);
            return NoContent();
        }
    }
}