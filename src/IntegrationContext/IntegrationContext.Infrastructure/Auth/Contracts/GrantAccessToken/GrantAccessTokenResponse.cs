namespace IntegrationContext.Infrastructure.Auth.Contracts.GrantAccessToken;

public record GrantAccessTokenResponse(
	string access_token,
	string token_type,
	string refresh_token,
	long expires_in
);
