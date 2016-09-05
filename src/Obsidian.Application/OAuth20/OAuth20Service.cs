using Microsoft.IdentityModel.Tokens;
using Obsidian.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obsidian.Application.OAuth20
{
    public class OAuth20Service
    {
        private readonly string _audience;
        private readonly string _issuer;
        private readonly SymmetricSecurityKey _singingKey;

        public OAuth20Service(OAuth20Configuration config)
        {
            var key = config.GetTokenSigningKey();
            _singingKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(key));
            _audience = config.GetTokenAudience();
            _issuer = config.GetTokenIssuer();
        }

        public string GenerateAccessToken(User user, IEnumerable<PermissionScope> scopes)
        {
            var signingCredentials = new SigningCredentials(_singingKey, SecurityAlgorithms.HmacSha256);
            var claims = user.GetClaims(scopes);
            var jwt = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddMinutes(5)
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
    }
}
