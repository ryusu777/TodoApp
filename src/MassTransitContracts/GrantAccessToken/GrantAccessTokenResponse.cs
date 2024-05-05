namespace MassTransitContracts.GrantAccessToken;

public record GrantAccessTokenResponse(
    bool Granted, 
    string Username, 
    string Email,
    string? ErrorCode, 
    string? ErrorDescription);
