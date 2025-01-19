namespace EstiPro.Application.Options;

public sealed class Auth0Options
{
    public const string SectionName = "Auth0";
    public string Connection { get; set; } = null!;
    public string Domain { get; set; } = null!;
    public string PureDomain { get; set; } = null!;
    public Auth0Api EstiPro { get; set; } = null!;
    public Auth0Api EstiProApi { get; set; } = null!;
}

public sealed class Auth0Api
{
    public string Audience { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string GrantType { get; set; } = null!;
}