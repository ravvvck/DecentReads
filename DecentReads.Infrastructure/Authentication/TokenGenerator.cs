
using DecentReads.Application.Common.Authentication;
using DecentReads.Application.Common.Interfaces.Services;
using DecentReads.Domain.Users;
using DecentReads.Domain.Users.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Infrastructure.Authentication
{
    internal class TokenGenerator : ITokenGenerator
    {
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly JwtSettings jwtSettings;

        public TokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings)
        {
            this.dateTimeProvider = dateTimeProvider;
            this.jwtSettings = jwtSettings.Value;
        }
        
        public string GenerateToken(User user)
        {
            var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Username}"),
                new Claim(ClaimTypes.Role, $"{user.Role}")

            };

            var securityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: dateTimeProvider.UtcNow.AddDays(jwtSettings.ExpiryDays),
                claims: claims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddMinutes(1),
                Created = DateTime.Now
            };
            return refreshToken;
        }
    }
}
