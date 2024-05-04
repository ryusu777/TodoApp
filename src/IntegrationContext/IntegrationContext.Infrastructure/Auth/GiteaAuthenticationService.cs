using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using IntegrationContext.Application.Auth;
using IntegrationContext.Application.Auth.Models;
using IntegrationContext.Domain.Auth;
using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Infrastructure.Auth.Contracts.GrantAccessToken;
using IntegrationContext.Infrastructure.Auth.Contracts.RefreshAccessToken;
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

    private string BaseUrl => $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/authorize-gitea";

    public GiteaAuthenticationService(
        IHttpClientFactory httpFactory, 
        IOptions<GiteaClientCredentials> credentials, 
        IHttpContextAccessor httpContextAccessor, 
        IUserRepository userRepository)
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

    public async Task<Result<GiteaAuthenticationResult>> GrantAccessTokenAsync(string verifyCode, CancellationToken ct)
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

    public async Task<Result<GiteaAuthenticatedUser>> GetAuthenticatedUser(JwtToken token, CancellationToken ct)
    {
        var client = _httpFactory.CreateClient(CLIENT_NAME);

        client.DefaultRequestHeaders
            .Add("Authorization", "token " + token.Value);

        var result = await client.GetFromJsonAsync<GiteaAuthenticatedUser>("api/v1/user", ct);

        if (result is null)
            return Result
                .Failure<GiteaAuthenticatedUser>(AuthDomainError.FailedToFetchGiteaUser);

        return Result.Success(result);
    }

    public Result<DateTime> GetExpiredDateTime(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        JwtSecurityToken readToken = handler.ReadJwtToken(token);

        Claim? expClaim = readToken
            .Claims
            .FirstOrDefault(e => e.Type == JwtRegisteredClaimNames.Exp);

        if (expClaim is null)
            return Result.Failure<DateTime>(AuthDomainError.InvalidToken);

        return Result.Success(readToken.ValidTo);
    }
}

