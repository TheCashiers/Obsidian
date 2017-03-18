using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Obsidian.Application.Cryptography;
using Obsidian.Domain;
using Obsidian.Domain.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Obsidian.Application.OAuth20
{
    public class OAuth20Service
    {
        private readonly AsymmetricSecurityKey _signingKey;
        private readonly OAuth20Configuration _config;
        private readonly ClaimService _claimService;
        private readonly RsaSigningService _rsaService;

        public OAuth20Service(IOptions<OAuth20Configuration> options, ClaimService claimSvc, RsaSigningService signingService)
        {
            _config = options.Value;
            _claimService = claimSvc;
            _rsaService = signingService;
            _signingKey = new RsaSecurityKey(_rsaService.GetPublicAndPrivateKey());
        }

        public string GenerateAccessToken(User user, IEnumerable<PermissionScope> scopes)
        {
            var signingCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.RsaSha512Signature);
            var claims = _claimService.GetClaims(user, scopes);
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
                AuthenticationType = AuthenticationSchemes.Bearer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,
                ValidateIssuer = true,
                ValidIssuer = _config.TokenIssuer,
                ValidAudience = _config.TokenAudience
            };
            var principal = handler.ValidateToken(token, param, out var vt);
            return principal != null;
        }
    }
}