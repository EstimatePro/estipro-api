using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EstiPro.Api.Extensions;
using Microsoft.IdentityModel.Tokens;
using EstiPro.Application.DTOs.Users;

namespace EstiPro.Api.IntegrationTests.Fixtures;

internal static class JwtTokenHelper
{
    private static readonly JwtSecurityTokenHandler SecurityTokenHandler = new();

    public static string GetJwtToken(UserDto user)
    {
        return GetJwtToken(
            new List<Claim>
            {
                new($"{EstiProClaimTypes.UserGuid}", user.Id.ToString()),
                new($"{EstiProClaimTypes.UserNickName}", user.NickName)
            });
    }

    private static string GetJwtToken(IEnumerable<Claim> claims)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddDays(1.0),
            Subject = new ClaimsIdentity(claims)
        };
        var token = SecurityTokenHandler.CreateToken(tokenDescriptor);
        return SecurityTokenHandler.WriteToken(token);
    }
}
