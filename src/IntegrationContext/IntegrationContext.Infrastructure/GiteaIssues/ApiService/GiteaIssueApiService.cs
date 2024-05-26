using System.Net.Http.Json;
using IntegrationContext.Application.GiteaIssues;
using IntegrationContext.Application.GiteaRepositories;
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
            $"repos/{repository.RepoOwner.Value}/{repository.RepoName}/issues", 
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
            repository.Id,
            response.UpdateAt
        );

        return Result.Success(createdIssue);
    }

    public async Task<Result<string>> UpdateIssueAsync(
        JwtToken jwt, 
        AssignmentUpdatedMessage message, 
        UserId repoOwner,
        string repoName,
        IssueNumber issueNumber,
        CancellationToken ct)
    {
        var client = _httpFactory.CreateClient(CLIENT_NAME);

        client.DefaultRequestHeaders.Add("Authorization", "token " + jwt.Value);

        var result = await client.PatchAsJsonAsync(
            $"repos/{repoOwner.Value}/{repoName}/issues/{issueNumber.Value}", 
            new CreateIssueRequest
            {
                Assignees = message.Assignees,
                Body = message.Description,
                DueDate = message.Deadline?.ToString("yyyy-MM-ddThh:mm:ss.fffZ"),
                Title = message.Title
            },
            ct);

        if (!result.IsSuccessStatusCode)
            return Result.Failure<string>(new Error(
                GiteaIssueDomainError.FailedToUpdateIssue.Code,
                await result.Content.ReadAsStringAsync()
            ));

        var response = await result.Content.ReadFromJsonAsync<UpdateIssueResponse>();

        if (response is null)
            return Result.Failure<string>(GiteaIssueDomainError.FailedToUpdateIssue);

        return Result.Success(response.UpdatedAt);
    }

    public async Task<Result> DeleteIssueAsync(
        JwtToken jwt, 
        AssignmentDeletedMessage message, 
        UserId repoOwner,
        string repoName,
        IssueNumber issueNumber,
        CancellationToken ct)
    {
        var client = _httpFactory.CreateClient(CLIENT_NAME);

        client.DefaultRequestHeaders.Add("Authorization", "token " + jwt.Value);

        var result = await client.DeleteAsync(
            $"repos/{repoOwner.Value}/{repoName}/issues/{issueNumber.Value}", 
            ct);

        if (!result.IsSuccessStatusCode)
            return Result.Failure<GiteaIssue>(GiteaIssueDomainError.FailedToDeleteIssue(await result.Content.ReadAsStringAsync()));

        return Result.Success();
    }

    public async Task<Result<string>> CloseIssueAsync(JwtToken jwt, AssignmentCompletedMessage message, UserId repoOwner, string repoName, IssueNumber issueNumber, CancellationToken ct)
    {
        var client = _httpFactory.CreateClient(CLIENT_NAME);

        client.DefaultRequestHeaders.Add("Authorization", "token " + jwt.Value);

        var result = await client.PatchAsJsonAsync(
            $"repos/{repoOwner.Value}/{repoName}/issues/{issueNumber.Value}", 
            new CloseIssueRequest(),
            ct);

        var response = await result.Content.ReadFromJsonAsync<UpdateIssueResponse>();

        if (!result.IsSuccessStatusCode || response is null)
            return Result.Failure<string>(GiteaIssueDomainError.FailedToCloseIssue(await result.Content.ReadAsStringAsync()));

        return Result.Success(response.UpdatedAt);
    }

    public async Task<Result<string>> ReopenIssueAsync(JwtToken jwt, AssignmentRenewedMessage message, UserId repoOwner, string repoName, IssueNumber issueNumber, CancellationToken ct)
    {
        var client = _httpFactory.CreateClient(CLIENT_NAME);

        client.DefaultRequestHeaders.Add("Authorization", "token " + jwt.Value);

        var result = await client.PatchAsJsonAsync(
            $"repos/{repoOwner.Value}/{repoName}/issues/{issueNumber.Value}", 
            new ReopenIssueRequest(),
            ct);

        var response = await result.Content.ReadFromJsonAsync<UpdateIssueResponse>();

        if (!result.IsSuccessStatusCode || response is null)
            return Result.Failure<string>(GiteaIssueDomainError.FailedToCloseIssue(await result.Content.ReadAsStringAsync()));

        return Result.Success(response.UpdatedAt);
    }
}
