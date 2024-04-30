using System.Net.Http.Json;
using System.Text.Json.Serialization;
using IntegrationContext.Application.Auth;
using IntegrationContext.Application.Auth.Models;
using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Infrastructure.Auth.Contracts;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace IntegrationContext.Infrastructure.Auth;

public class GiteaAuthenticationService : IGiteaAuthenticationService
{
    public const string CLIENT_NAME = "gitea";

    private readonly IHttpClientFactory _httpFactory;
    private readonly GiteaClientCredentials _credentials;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;

    private string BaseUrl => $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

    public GiteaAuthenticationService(IHttpClientFactory httpFactory, IOptions<GiteaClientCredentials> credentials, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _httpFactory = httpFactory;
        _credentials = credentials.Value;
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    public Task<Result<GiteaClientCredentials>> GetClientCredentials(CancellationToken ct)
    {
        return Task.FromResult(Result.Success(_credentials));
    }

    public async Task<Result<GiteaAuthenticationResult>> GrantAccessTokenAsync(UserId username, string verifyCode, CancellationToken ct)
    {
        var client = _httpFactory.CreateClient(CLIENT_NAME);

        var result = await client.PostAsJsonAsync("/login/oauth/access_token", new GrantAccessTokenRequest(
            _credentials.ClientId,
            _credentials.ClientSecret,
            verifyCode,
            "authorization_code",
            BaseUrl
        ), ct);

        var credential = await result.Content.ReadFromJsonAsync<GiteaAuthenticationResult>();

        if (credential is null)
            return Result
                .Failure<GiteaAuthenticationResult>(UserInfrastructureError.FailedToAuthenticateGitea);

        return Result.Success(credential);
    }

    public async Task<Result<GiteaAuthenticationResult>> RefreshTokenAsync(UserId username, CancellationToken ct)
    {
        var client = _httpFactory.CreateClient(CLIENT_NAME);

        var userResult = await _userRepository
            .GetGiteaUserByUsername(username, ct);

        if (userResult.IsFailure || userResult.Value is null)
            return Result
                .Failure<GiteaAuthenticationResult>(UserInfrastructureError.UserNotFound);

        var refreshToken = userResult.Value.RefreshToken;

        if (refreshToken is null)
            return Result
                .Failure<GiteaAuthenticationResult>(UserInfrastructureError.RefreshTokenIsNull);

        var result = await client.PostAsJsonAsync("/login/oauth/access_token", new RefreshAccessTokenRequest(
            _credentials.ClientId,
            _credentials.ClientSecret,
            "authorization_code",
            refreshToken.Value
        ), ct);

        var credential = await result.Content.ReadFromJsonAsync<GiteaAuthenticationResult>();

        if (credential is null)
            return Result
                .Failure<GiteaAuthenticationResult>(UserInfrastructureError.FailedToAuthenticateGitea);

        return Result.Success(credential);
    }
}

