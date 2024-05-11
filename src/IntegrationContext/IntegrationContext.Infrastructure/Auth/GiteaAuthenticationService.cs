using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.Auth;
using IntegrationContext.Application.Auth.Models;
using IntegrationContext.Domain.Auth;
using IntegrationContext.Domain.Auth.Events;
using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Infrastructure.Auth.Contracts.GrantAccessToken;
using IntegrationContext.Infrastructure.Auth.Contracts.RefreshAccessToken;
using Library.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace IntegrationContext.Infrastructure.Auth;

public class GiteaAuthenticationService : IGiteaAuthenticationService
{
    public const string CLIENT_NAME = "gitea";

    private readonly IHttpClientFactory _httpFactory;
    private readonly GiteaClientCredentials _credentials;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly string _clientUrl;
    private string BaseUrl => $"{_clientUrl}/authorize-gitea";

    public GiteaAuthenticationService(
        IConfiguration config,
        IHttpClientFactory httpFactory,
        IOptions<GiteaClientCredentials> credentials,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _httpFactory = httpFactory;
        _credentials = credentials.Value;
        _userRepository = userRepository;

        var url = config["GiteaUrl"];
        if (url is null)
            throw new Exception("Gitea url is not found, please set Gitea Url in configuration");

        var clientUrl = config["ClientUrl"];
        if (clientUrl is null)
            throw new Exception("Client url is not found, please set Client Url in configuration");

        _clientUrl = clientUrl;
        _unitOfWork = unitOfWork;
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

        if (!result.IsSuccessStatusCode)
            return Result
                .Failure<GiteaAuthenticationResult>(UserInfrastructureError.FailedToAuthenticateGitea);

        var credential = await result.Content.ReadFromJsonAsync<GrantAccessTokenResponse>();

        if (credential is null)
            return Result
                .Failure<GiteaAuthenticationResult>(UserInfrastructureError.FailedToAuthenticateGitea);

        return Result.Success(new GiteaAuthenticationResult(
            JwtToken.Create(credential.access_token),
            RefreshToken.Create(credential.refresh_token),
            GetExpiredDateTime(credential.access_token).Value,
            GetExpiredDateTime(credential.refresh_token).Value
		));
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

        var credential = await result.Content.ReadFromJsonAsync<GrantAccessTokenResponse>();

        if (credential is null)
            return Result
                .Failure<GiteaAuthenticationResult>(UserInfrastructureError.FailedToAuthenticateGitea);

        return Result.Success(new GiteaAuthenticationResult(
            JwtToken.Create(credential.access_token),
            RefreshToken.Create(credential.refresh_token),
            GetExpiredDateTime(credential.access_token).Value,
            GetExpiredDateTime(credential.refresh_token).Value
		));
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

    public async Task<Result<string>> GetUserJwt(UserId userId, CancellationToken ct)
    {
        var user = await _userRepository.GetGiteaUserByUsername(userId, ct);

        if (user.IsFailure || user.Value is null)
            return Result.Failure<string>(user.Error);

        if (user.Value.JwtToken is null || user.Value.RefreshToken is null)
            return Result.Failure<string>(AuthDomainError.GiteaUserNotAuthenticated);

        var userJwtExpiryDate = GetExpiredDateTime(user.Value.JwtToken.Value);

        if (userJwtExpiryDate.IsFailure)
            return Result.Failure<string>(userJwtExpiryDate.Error);

        var userRefreshTokenExpiryDate = GetExpiredDateTime(user.Value.RefreshToken.Value ?? "");

        if (userRefreshTokenExpiryDate.IsFailure)
            return Result.Failure<string>(userRefreshTokenExpiryDate.Error);

        if (userJwtExpiryDate.Value > DateTime.UtcNow)
            return Result.Success(user.Value.JwtToken.Value);

        if (userJwtExpiryDate.Value <= DateTime.UtcNow && userRefreshTokenExpiryDate.Value > DateTime.UtcNow)
        {
            var refreshTokenResult = await RefreshTokenAsync(userId, ct);

            if (refreshTokenResult.IsFailure || refreshTokenResult.Value is null)
                return Result.Failure<string>(refreshTokenResult.Error);

            _unitOfWork.AddEventQueue(new AccessTokenGranted(
                refreshTokenResult.Value.JwtToken, refreshTokenResult.Value.RefreshToken));

            return Result.Success(refreshTokenResult.Value.JwtToken.Value);
        }
        
        return Result.Failure<string>(AuthDomainError.GiteaUserNotAuthenticated);
    }
}

