namespace AuthContext.Application.Identity.Model;

public class JwtOptions
{
    public const string OptionSection = "JwtOptions";
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required string SecretKey { get; set; }
    public required int JwtExpiresInMinutes { get; set; }
    public required int RefreshTokenExpiresInDays { get; set; }
}
