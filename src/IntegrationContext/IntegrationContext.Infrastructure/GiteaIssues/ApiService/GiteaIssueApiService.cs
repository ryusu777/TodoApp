using System.Net.Http.Json;
using IntegrationContext.Application.GiteaIssues;
using IntegrationContext.Application.GiteaRepositories;
using IntegrationContext.Application.GiteaRepositories.Dtos;
using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Domain.GiteaIssues;
using IntegrationContext.Domain.GiteaIssues.ValueObjects;
using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using IntegrationContext.Infrastructure.GiteaIssues.ApiService.Contracts.CreateIssue;
using Library.Models;
using MassTransitContracts.ProjectManagement.Assignments;

namespace IntegrationContext.Infrastructure.GiteaIssues.ApiService;

public class GiteaIssueApiService : IGiteaIssueApiService
{
    public const string CLIENT_NAME = "gitea-api";
    private readonly IHttpClientFactory _httpFactory;
    private IGiteaRepositoryRepository _repoRepository;

    public GiteaIssueApiService(IGiteaRepositoryRepository repoRepository, IHttpClientFactory httpFactory)
    {
        _repoRepository = repoRepository;
        _httpFactory = httpFactory;
    }

    public async Task<Result<GiteaIssue>> CreateIssueAsync(JwtToken jwt, AssignmentCreatedMessage message, CancellationToken ct)
    {
        var repoResult = await _repoRepository.GetProjectRepositoryByIdAsync(
            GiteaRepositoryId.Create(message.GiteaRepositoryId),
            ProjectId.Create(message.ProjectId),
            ct
        );

        if (repoResult.Value is null)
            return Result.Failure<GiteaIssue>(repoResult.Error);

        GiteaRepositoryDto repository = repoResult.Value;

        var client = _httpFactory.CreateClient(CLIENT_NAME);

        client.DefaultRequestHeaders.Add("Authorization", "token " + jwt.Value);

        var result = await client.PostAsJsonAsync(
            $"repos/{repository.RepoOwner}/{repository.RepoName}/issues", 
            new CreateIssueRequest
            {
                Assignees = message.Assignees,
                Body = message.Description,
                DueDate = message.Deadline,
                Title = message.Title
            },
            ct);

        var response = await result.Content.ReadFromJsonAsync<CreateIssueResponse>();

        if (response is null || !result.IsSuccessStatusCode)
            return Result.Failure<GiteaIssue>(GiteaIssueDomainError.FailedToCreateIssue);

        GiteaIssue createdIssue = GiteaIssue.Create(
            GiteaIssueId.Create(response.Id),
            IssueNumber.Create(response.IssueNumber),
            AssignmentId.Create(message.Id),
            GiteaRepositoryId.Create(repository.Id)
        );

        return Result.Success(createdIssue);
    }
}
