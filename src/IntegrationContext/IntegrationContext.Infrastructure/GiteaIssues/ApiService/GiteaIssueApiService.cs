using System.Net.Http.Json;
using IntegrationContext.Application.GiteaIssues;
using IntegrationContext.Application.GiteaRepositories;
using IntegrationContext.Application.GiteaRepositories.Dtos;
using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Domain.GiteaIssues;
using IntegrationContext.Domain.GiteaIssues.ValueObjects;
using IntegrationContext.Domain.GiteaRepositories;
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
    private IGiteaIssueRepository _issueRepository;

    public GiteaIssueApiService(IGiteaRepositoryRepository repoRepository, IHttpClientFactory httpFactory, IGiteaIssueRepository issueRepository)
    {
        _repoRepository = repoRepository;
        _httpFactory = httpFactory;
        _issueRepository = issueRepository;
    }

    public async Task<Result<GiteaIssue>> CreateIssueAsync(JwtToken jwt, AssignmentCreatedMessage message, CancellationToken ct)
    {
        var repoResult = await _repoRepository.GetProjectRepositoryByIdAsync(
            GiteaRepositoryId.Create(message.GiteaRepositoryId),
            ct
        );

        if (repoResult.Value is null)
            return Result.Failure<GiteaIssue>(repoResult.Error);

        GiteaRepository repository = repoResult.Value;

        var client = _httpFactory.CreateClient(CLIENT_NAME);

        client.DefaultRequestHeaders.Add("Authorization", "token " + jwt.Value);

        var result = await client.PostAsJsonAsync(
            $"repos/{repository.RepoOwner}/{repository.RepoName}/issues", 
            new CreateIssueRequest
            {
                Assignees = message.Assignees,
                Body = message.Description,
                DueDate = message.Deadline?.ToString("yyyy-MM-ddThh:mm:ss.fffZ"),
                Title = message.Title
            },
            ct);

        if (!result.IsSuccessStatusCode)
            return Result.Failure<GiteaIssue>(new Error(
                GiteaIssueDomainError.FailedToCreateIssue.Code,
                await result.Content.ReadAsStringAsync()
            ));

        var response = await result.Content.ReadFromJsonAsync<CreateIssueResponse>();

        if (response is null)
            return Result.Failure<GiteaIssue>(GiteaIssueDomainError.FailedToCreateIssue);

        GiteaIssue createdIssue = GiteaIssue.Create(
            GiteaIssueId.Create(response.Id),
            IssueNumber.Create(response.IssueNumber),
            AssignmentId.Create(message.Id),
            repository.Id
        );

        return Result.Success(createdIssue);
    }

    public async Task<Result> UpdateIssueAsync(JwtToken jwt, AssignmentUpdatedMessage message, CancellationToken ct)
    {
        var issueResult = await _issueRepository
            .GetIssueByAssignmentId(AssignmentId.Create(message.Id), ct);

        if (issueResult.Value is null)
            return Result.Failure<GiteaIssue>(issueResult.Error);

        GiteaIssue issue = issueResult.Value;

        var repoResult = await _repoRepository.GetProjectRepositoryByIdAsync(
            issue.GiteaRepositoryId,
            ct
        );

        if (repoResult.Value is null)
            return Result.Failure<GiteaIssue>(repoResult.Error);

        GiteaRepository repository = repoResult.Value;

        var client = _httpFactory.CreateClient(CLIENT_NAME);

        client.DefaultRequestHeaders.Add("Authorization", "token " + jwt.Value);

        var result = await client.PatchAsJsonAsync(
            $"repos/{repository.RepoOwner}/{repository.RepoName}/issues/{issue.IssueNumber.Value}", 
            new CreateIssueRequest
            {
                Assignees = message.Assignees,
                Body = message.Description,
                DueDate = message.Deadline?.ToString("yyyy-MM-ddThh:mm:ss.fffZ"),
                Title = message.Title
            },
            ct);

        if (!result.IsSuccessStatusCode)
            return Result.Failure(new Error(
                GiteaIssueDomainError.FailedToUpdateIssue.Code,
                await result.Content.ReadAsStringAsync()
            ));

        var response = await result.Content.ReadFromJsonAsync<UpdateIssueResponse>();

        if (response is null)
            return Result.Failure(GiteaIssueDomainError.FailedToUpdateIssue);

        return Result.Success();
    }

    public async Task<Result<GiteaIssue>> DeleteIssueAsync(JwtToken jwt, AssignmentDeletedMessage message, CancellationToken ct)
    {
        var issueResult = await _issueRepository
            .GetIssueByAssignmentId(AssignmentId.Create(message.Id), ct);

        if (issueResult.Value is null)
            return Result.Failure<GiteaIssue>(issueResult.Error);

        GiteaIssue issue = issueResult.Value;

        var repoResult = await _repoRepository.GetProjectRepositoryByIdAsync(
            issue.GiteaRepositoryId,
            ct
        );

        if (repoResult.Value is null)
            return Result.Failure<GiteaIssue>(repoResult.Error);

        GiteaRepository repository = repoResult.Value;

        var client = _httpFactory.CreateClient(CLIENT_NAME);

        client.DefaultRequestHeaders.Add("Authorization", "token " + jwt.Value);

        var result = await client.DeleteAsync(
            $"repos/{repository.RepoOwner}/{repository.RepoName}/issues/{issue.IssueNumber.Value}", 
            ct);

        if (!result.IsSuccessStatusCode)
            return Result.Failure<GiteaIssue>(GiteaIssueDomainError.FailedToDeleteIssue);

        return Result.Success(issue);
    }
}
