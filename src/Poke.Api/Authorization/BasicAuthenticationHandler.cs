using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Poke.Api.Authorization;

public class BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IConfiguration configuration)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authorizationHeader = Request.Headers.Authorization;

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.ToString().StartsWith("Basic "))
        {
            return Task.FromResult(AuthenticateResult.Fail("Missing or invalid Authorization header."));
        }

        try
        {
            var encodedCredentials = authorizationHeader.ToString()["Basic ".Length..].Trim();
            var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
            var credentials = decodedCredentials.Split(':');

            var username = credentials[0];
            var password = credentials[1];

            if (username != configuration["AuthBasic:UserName"] || password != configuration["AuthBasic:Password"])
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
            }

            var claims = new[] { new System.Security.Claims.Claim("name", username) };
            var identity = new System.Security.Claims.ClaimsIdentity(claims, "Basic");
            var principal = new System.Security.Claims.ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, "Basic");
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization header"));
        }
    }
}