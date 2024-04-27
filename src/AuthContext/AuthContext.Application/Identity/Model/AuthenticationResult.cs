namespace AuthContext.Application.Identity.Model;

public record AuthenticationResult(
    string access_token,
    long expires_in,
    string token_type,
    string refresh_token
);
