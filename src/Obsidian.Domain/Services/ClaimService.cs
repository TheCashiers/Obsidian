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
        {
            var userClaims = GetClaimsFromObjectByType(userClaimGetters, claimTypes, user);
            var profileClaims = GetClaimsFromObjectByType(userProfileClaimGetters, claimTypes, user.Profile);
            return userClaims.Union(profileClaims).Union(user.Claims);
        }

        private static IEnumerable<PropertyClaimGetter> userClaimGetters
                    = MetadataHelper.GetClaimPropertyGetters<User>();

        private static IEnumerable<PropertyClaimGetter> userProfileClaimGetters
                    = MetadataHelper.GetClaimPropertyGetters<UserProfile>();

        private static IEnumerable<Claim> GetClaimsFromObjectByType(IEnumerable<PropertyClaimGetter> getters, IEnumerable<string> claimTypes, object obj)
                => getters.Join(claimTypes, g => g.ClaimType, ct => ct, (g, ct) => g).Select(g => g.GetClaim(obj));
    }
}