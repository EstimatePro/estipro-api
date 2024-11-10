using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Poke.Application.DTOs.Users;
using Poke.Presentation.Extensions;

namespace Poke.Api.IntegrationTests.Fixtures;

internal static class JwtTokenHelper
{
    private static readonly JwtSecurityTokenHandler SecurityTokenHandler = new();

    public static string GetJwtToken(UserDto user)
    {
        return GetJwtToken(
            new List<Claim>
            {
                new($"{PokeClaimTypes.UserGuid}", user.Id.ToString()),
                new($"{PokeClaimTypes.UserNickName}", user.NickName)
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
