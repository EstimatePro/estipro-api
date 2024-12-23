using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EstiPro.Api.IntegrationTests.Extensions;

public class TestAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string AuthenticationScheme = "TestScheme";
    private static readonly JwtSecurityTokenHandler STokenHandler = new();

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var token = Context.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);
        IEnumerable<Claim>? claims = STokenHandler.ReadJwtToken(token).Claims;
        var identity = new ClaimsIdentity(claims, AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, AuthenticationScheme);

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}
