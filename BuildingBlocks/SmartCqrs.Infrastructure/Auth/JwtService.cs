using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartCqrs.Infrastructure.Configuration;

namespace SmartCqrs.Infrastructure.Auth
{
    public class JwtService : IJwtService
    {
        private readonly AppSettings _appSettings;
        public JwtService(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        public string GenerateToken(ClientUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.IdentityServer.JwtSecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "test",
                Audience = "api",
                Subject = new ClaimsIdentity(new Claim[]
                {
                    
                    //new Claim(JwtClaimTypes.Subject, user.Sub),
                    new Claim(JwtClaimTypes.Id, user.UUID.ToString()),
                    //new Claim("name", user.Nickname),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public bool ValidateToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
