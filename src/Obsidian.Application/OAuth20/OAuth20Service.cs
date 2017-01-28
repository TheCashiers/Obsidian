using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Obsidian.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Obsidian.Application.OAuth20
{
    public class OAuth20Service
    {
        private readonly SymmetricSecurityKey _singingKey;
        private readonly OAuth20Configuration _config;

        public OAuth20Service(IOptions<OAuth20Configuration> options)
        {
            _config = options.Value;
            _singingKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(_config.TokenSigningKey));
        }

        public string GenerateAccessToken(User user, IEnumerable<PermissionScope> scopes)
        {
            var signingCredentials = new SigningCredentials(_singingKey, SecurityAlgorithms.HmacSha256);
            var claims = user.GetClaims(scopes);
            var jwt = new JwtSecurityToken(
                issuer: _config.TokenIssuer,
                audience: _config.TokenAudience,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddMinutes(5)
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
    }
}