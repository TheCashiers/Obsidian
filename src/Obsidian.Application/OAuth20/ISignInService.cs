using System.Threading.Tasks;
using Obsidian.Domain;

namespace Obsidian.Application.OAuth20
{
    public interface ISignInService
    {
        Task CookieSignInAsync(User user, bool isPersistent);

        Task CookieSignOutCurrentUserAsync();
    }
}