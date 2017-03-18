using Obsidian.Domain.Misc;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace Obsidian.Domain.Services
{
    public class ClaimService
    {
        public IEnumerable<Claim> GetClaims(User user, IEnumerable<PermissionScope> scopes)
                => GetClaims(user, scopes.SelectMany(s => s.ClaimTypes).Distinct());

        public IEnumerable<Claim> GetClaims(User user, IEnumerable<string> claimTypes)
                => GetAllClaims(user).Join(claimTypes, c => c.Type, ct => ct, (c, ct) => c);

        private IEnumerable<Claim> GetAllClaims(User user)
        {
            var userClaims = GetClaimsFromObject(userClaimGetters, user);
            var profileClaims = GetClaimsFromObject(userProfileClaimGetters, user.Profile);
            return userClaims.Union(profileClaims).Union(user.Claims);
        }

        private static IEnumerable<PropertyClaimGetter> userClaimGetters
                    = MetadataHelper.GetClaimPropertyGetters<User>();

        private static IEnumerable<PropertyClaimGetter> userProfileClaimGetters
                    = MetadataHelper.GetClaimPropertyGetters<UserProfile>();

        private static IEnumerable<Claim> GetClaimsFromObject(IEnumerable<PropertyClaimGetter> getters, object obj)
                => getters.Select(g => g.GetClaim(obj));
    }
}