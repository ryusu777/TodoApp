namespace IntegrationContext.Infrastructure.Auth.Contracts;

public record RefreshAccessTokenRequest(
    string client_id,
    string client_secret,
    string grant_type,
    string refresh_token
);
