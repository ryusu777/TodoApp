namespace IntegrationContext.Infrastructure.Auth.Contracts;

public record GrantAccessTokenRequest(
    string client_id,
    string client_secret,
    string code,
    string grant_type,
    string redirect_uri
);
