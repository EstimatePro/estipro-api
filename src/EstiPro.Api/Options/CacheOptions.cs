namespace EstiPro.Api.Options;

public class CacheOptions
{
    public const string SectionName = "Cache";

    public TimeSpan Duration { get; set; }
}