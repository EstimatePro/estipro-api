namespace EstiPro.Application.Options;

public sealed class Auth0Options
{
    public const string SectionName = "Auth0";

    public string Audience { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public string Connection { get; set; } = null!;

    public string Domain { get; set; } = null!;

    public string ClientSecret { get; set; } = null!;
}
