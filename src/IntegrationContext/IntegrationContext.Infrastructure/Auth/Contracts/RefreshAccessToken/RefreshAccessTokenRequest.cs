namespace IntegrationContext.Infrastructure.Auth.Contracts.RefreshAccessToken;

public record RefreshAccessTokenRequest(
    string client_id,
    string client_secret,
    string grant_type,
    string refresh_token
);
