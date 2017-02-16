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
        private readonly SymmetricSecurityKey _signingKey;
        private readonly OAuth20Configuration _config;

        public OAuth20Service(IOptions<OAuth20Configuration> options)
        {
            _config = options.Value;
            _signingKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(_config.TokenSigningKey));
        }

        public string GenerateAccessToken(User user, IEnumerable<PermissionScope> scopes)
        {
            var signingCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
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

        public bool VerifyToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var param = new TokenValidationParameters
            {
                AuthenticationType = "Bearer",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,
                ValidateIssuer = true,
                ValidIssuer = _config.TokenIssuer,
                ValidAudience = _config.TokenAudience
            };
            SecurityToken vt;
            var principal = handler.ValidateToken(token, param, out vt);
            return principal != null;
        }
    }
}