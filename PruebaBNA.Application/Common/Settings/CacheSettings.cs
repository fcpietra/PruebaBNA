namespace PruebaBNA.Application.Common.Settings;

public class CacheSettings
{
    public const string SectionName = "CacheSettings";
    public int ExpirationInMinutes { get; set; }
    public int BcraExpirationSeconds { get; set; } = 3600; // Default 1 hora
}