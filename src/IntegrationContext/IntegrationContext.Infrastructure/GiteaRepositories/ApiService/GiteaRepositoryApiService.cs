using System.Net.Http.Json;
using IntegrationContext.Application.Auth;
using IntegrationContext.Application.GiteaRepositories;
using IntegrationContext.Application.GiteaRepositories.Dtos;
using IntegrationContext.Application.Pagination.Models;
using IntegrationContext.Domain.GiteaRepositories;
using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using IntegrationContext.Infrastructure.GiteaApiService.GiteaRepository.Contracts.CreateRepositoryWebhook;
using IntegrationContext.Infrastructure.GiteaApiService.GiteaRepository.Contracts.GetRepositories;
using Library.Models;
using Microsoft.Extensions.Configuration;

namespace IntegrationContext.Infrastructure.GiteaRepositories.ApiService;

public class GiteaRepositoryService : IGiteaRepositoryService
{
    public const string CLIENT_NAME = "gitea";

    private readonly IHttpClientFactory _httpFactory;
    private readonly IUserRepository _userRepository;
    private readonly string _issueHookUrl;
    private readonly string _hookAuthToken;

    public GiteaRepositoryService(
		IConfiguration config,
        IHttpClientFactory httpFactory, 
        IUserRepository userRepository)
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
    }

    public async Task<Result<int>> CreateRepositoryHookAsync(ProjectId projectId, string RepoOwner, string RepoName, CancellationToken ct)
    {
        var client = _httpFactory.CreateClient(CLIENT_NAME);

        var result = await client.PostAsJsonAsync(
            $"/repos/{RepoOwner}/{RepoName}/hooks", 
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
            return Result.Failure<int>(GiteaRepositoryDomainError.FailedToCreateGiteaRepositoryHook);

        return Result.Success(response.Id);
    }

    public async Task<Result<List<GiteaRepositoryDto>>> GetGiteaRepositoriesAsync(string searchText, Paging? page, CancellationToken ct)
    {
        var client = _httpFactory.CreateClient(CLIENT_NAME);

        var response = await client
            .GetFromJsonAsync<GetRepositoriesResponse>("repos/search?" +
                (searchText is not null ? $"q={searchText}&" : "") +
                (page is not null ? $"page={page.Page}&limit={page.ItemPerPage}" : "")
            );

        if (response is null)
            return Result.Failure<List<GiteaRepositoryDto>>(GiteaRepositoryDomainError.FailedToGetGiteaRepositories);

        return Result
            .Success(response.Data
                .Select(e => new GiteaRepositoryDto(e.RepoOwner, e.RepoName))
                .ToList());
    }
}

