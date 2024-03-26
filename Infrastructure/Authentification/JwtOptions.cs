namespace Infrastructure.Authentification;

public sealed class JwtOptions
{
    public string? Issuer { get; init; } 
    public string? Audience { get; init; } 
    public required string SecretKey { get; init; }
    public int TokenExpirationHours { get; init; } = 8;
}