using System.Security.Claims;

namespace Poke.Presentation.Extensions;

public static class ClaimsExtensions
{
    public static Guid GetUserGuid(this ClaimsPrincipal principal)
    {
        return principal.GetFirstGuidClaim(PokeClaimTypes.UserGuid)
               ?? throw new InvalidOperationException($"ClaimsPrincipal has no {PokeClaimTypes.UserGuid} claim.");
    }

    public static Guid GetUserEmail(this ClaimsPrincipal principal)
    {
        return principal.GetFirstGuidClaim(PokeClaimTypes.UserEmail)
               ?? throw new InvalidOperationException($"ClaimsPrincipal has no {PokeClaimTypes.UserEmail} claim.");
    }

    public static string GetUserNickName(this ClaimsPrincipal principal)
    {
        return principal.GetFirstClaim(PokeClaimTypes.UserNickName)
               ?? throw new InvalidOperationException($"ClaimsPrincipal has no {PokeClaimTypes.UserNickName} claim.");
    }

    private static string? GetFirstClaim(this ClaimsPrincipal principal, string claimType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(claimType);
        return principal.FindFirst(claimType)?.Value;
    }

    private static Guid? GetFirstGuidClaim(this ClaimsPrincipal principal, string claimType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(claimType);

        var input = principal.FindFirst(claimType)?.Value;
        return input != null && Guid.TryParse(input, out var result)
            ? result
            : null;
    }
}

public static class PokeClaimTypes
{
    public const string UserGuid = "userGuid";
    public const string UserEmail = "userEmail";
    public const string UserNickName = "userNickName";
}
