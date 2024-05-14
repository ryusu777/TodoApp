using System.Net.Http.Json;
using IntegrationContext.Application.Auth;
using IntegrationContext.Application.GiteaRepositories;
using IntegrationContext.Application.GiteaRepositories.Dtos;
using IntegrationContext.Application.Pagination.Models;
using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Domain.GiteaRepositories;
using IntegrationContext.Domain.GiteaRepositories.Entities;
using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using IntegrationContext.Infrastructure.GiteaRepositories.ApiService.Contracts.CreateRepositoryWebhook;
using IntegrationContext.Infrastructure.GiteaRepositories.ApiService.Contracts.GetRepositories;
using Library.Models;
using Microsoft.Extensions.Configuration;

namespace IntegrationContext.Infrastructure.GiteaRepositories.ApiService;

public class GiteaRepositoryApiService : IGiteaRepositoryService
{
    public const string CLIENT_NAME = "gitea-api";

    private readonly IHttpClientFactory _httpFactory;
    private readonly IUserRepository _userRepository;
    private readonly IGiteaUserDomainService _userDomainService;
    private readonly string _issueHookUrl;
    private readonly string _hookAuthToken;

	public GiteaRepositoryApiService(
		IConfiguration config,
		IHttpClientFactory httpFactory,
		IUserRepository userRepository,
		IGiteaAuthenticationService authService,
		IGiteaUserDomainService userDomainService)
	{
		_httpFactory = httpFactory;
		_userRepository = userRepository;

		var hookUrl = config["Hooks:Issue"];
		if (hookUrl is null)
			throw new ArgumentNullException("Please set configuration for Hooks:Issue");

		var hookAuthToken = config["Hooks:AuthToken"];
		if (hookAuthToken is null)
			throw new ArgumentNullException("Please set configuration for Hooks:AuthToken");

		_issueHookUrl = hookUrl;
		_hookAuthToken = hookAuthToken;
		_userDomainService = userDomainService;
	}

	public async Task<Result<RepositoryHook>> CreateRepositoryHookAsync(JwtToken jwt, ProjectId projectId, string RepoOwner, string RepoName, CancellationToken ct)
    {
        var client = _httpFactory.CreateClient(CLIENT_NAME);

        client.DefaultRequestHeaders.Add("Authorization", "token " + jwt.Value);

        var result = await client.PostAsJsonAsync(
            $"repos/{RepoOwner}/{RepoName}/hooks", 
            new CreateRepositoryWebhookRequest
            {
                AuthorizationHeader = _hookAuthToken,
                Config = new CreateRepositoryWebhookRequest.Configuration
                {
                    Url = _issueHookUrl
                },
                Events = new string[] { "issues" },
            },
            ct);

        var response = await result.Content.ReadFromJsonAsync<CreateRepositoryWebhookResponse>();

        if (response is null || !result.IsSuccessStatusCode)
            return Result.Failure<RepositoryHook>(GiteaRepositoryDomainError.FailedToCreateGiteaRepositoryHook);

        return Result.Success(RepositoryHook.Create(
            RepositoryHookId.Create(response.Id),
            new Uri(response.Config.Url),
            response.Events.Select(e => HookEvent.Create(e)).ToList(),
            response.Active
        ));
    }

    public async Task<Result<List<GiteaRepositoryDto>>> GetGiteaRepositoriesAsync(
        JwtToken jwt, string? searchText, Paging? page, CancellationToken ct)
    {
        var client = _httpFactory.CreateClient(CLIENT_NAME);

        client.DefaultRequestHeaders.Add("Authorization", "token " + jwt.Value);

        var response = await client
            .GetFromJsonAsync<GetRepositoriesResponse>("repos/search?" +
                (searchText is not null ? $"q={searchText}&" : "") +
                (page is not null ? $"page={page.Page}&limit={page.ItemPerPage}" : "")
            );

        if (response is null)
            return Result.Failure<List<GiteaRepositoryDto>>(GiteaRepositoryDomainError.FailedToGetGiteaRepositories);

        return Result
            .Success(response.Data
                .Select(e => new GiteaRepositoryDto(e.Id, e.RepoOwner, e.RepoName))
                .ToList());
    }

    public async Task<Result<GiteaRepositoryDto>> GetGiteaRepositoryByOwnerAsync(UserId owner, string repoName, CancellationToken ct)
    {
        var client = _httpFactory.CreateClient(CLIENT_NAME);

        var response = await client
            .GetFromJsonAsync<GetRepositoryByOwnerResponse>($"repos/{owner.Value}/{repoName}");

        if (response is null)
            return Result.Failure<GiteaRepositoryDto>(GiteaRepositoryDomainError.FailedToGetGiteaRepositories);

        return Result
            .Success(new GiteaRepositoryDto(
                response.Id,
                response.RepoOwner,
                response.RepoName
            ));
    }
}

