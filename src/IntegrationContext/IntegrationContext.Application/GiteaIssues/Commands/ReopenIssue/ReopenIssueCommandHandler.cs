using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.Auth;
using IntegrationContext.Application.CommandOutboxes;
using IntegrationContext.Application.CommandOutboxes.CommandHandler;
using IntegrationContext.Application.GiteaRepositories;
using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Domain.GiteaIssues;
using IntegrationContext.Domain.GiteaIssues.Events;
using IntegrationContext.Domain.GiteaIssues.ValueObjects;
using IntegrationContext.Domain.GiteaRepositories;
using Library.Models;

namespace IntegrationContext.Application.GiteaIssues.Commands.ReopenIssue;

public class ReopenIssueCommandHandler : OutboxedCommandHandler<ReopenIssueCommand>
{
    private readonly IGiteaIssueApiService _issueApi;
    private readonly IGiteaUserDomainService _userDomainService;
    private readonly IGiteaIssueRepository _issueRepository;
    private readonly IGiteaRepositoryRepository _repoRepository;

    public ReopenIssueCommandHandler(
        IUnitOfWork unitOfWork,
        ICommandOutboxDomainService outboxDomainService,
        IGiteaIssueApiService issueApi,
        IGiteaUserDomainService userDomainService,
        IGiteaIssueRepository issueRepository,
        IGiteaRepositoryRepository repoRepository) : base(unitOfWork, outboxDomainService)
    {
        _issueApi = issueApi;
        _userDomainService = userDomainService;
        _issueRepository = issueRepository;
        _repoRepository = repoRepository;
    }

    protected override async Task<Result> HandleInternal(ReopenIssueCommand request, CancellationToken ct)
    {
        var user = await _userDomainService
            .GetOrRefreshJwt(UserId.Create(request.Message.UserId), ct);

        if (user.IsFailure || user.Value is null || user.Value.JwtToken is null)
            return user.Error;

        var issueResult = await _issueRepository
            .GetIssueByAssignmentIdAsync(AssignmentId.Create(request.Message.AssignmentId), ct);

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

        var interfaceResult = await _issueApi
            .ReopenIssueAsync(
                user.Value.JwtToken, 
                request.Message, 
                repository.RepoOwner,
                repository.RepoName,
                issue.IssueNumber,
                ct);

        if (interfaceResult.IsFailure || interfaceResult.Value is null)
            return interfaceResult;

        issue.Update(interfaceResult.Value);
        return await _unitOfWork.SaveChangesAsync(new GiteaIssueReopened(issue), ct);
    }
}
