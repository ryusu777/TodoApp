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

namespace IntegrationContext.Application.GiteaIssues.Commands.DeleteIssue;

public class DeleteIssueCommandHandler : OutboxedCommandHandler<DeleteIssueCommand>
{
    private readonly IGiteaIssueApiService _issueApi;
    private readonly IGiteaUserDomainService _userDomainService;
    private readonly IGiteaIssueRepository _issueRepository;
    private readonly IGiteaRepositoryRepository _repoRepository;

    public DeleteIssueCommandHandler(
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

    protected override async Task<Result> HandleInternal(
        DeleteIssueCommand request, CancellationToken ct)
    {
        var user = await _userDomainService
            .GetOrRefreshJwt(UserId.Create(request.Message.UserId), ct);

        if (user.IsFailure || user.Value is null || user.Value.JwtToken is null)
            return user.Error;

        var issueResult = await _issueRepository
            .GetIssueByAssignmentIdAsync(AssignmentId.Create(request.Message.Id), ct);

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
            .DeleteIssueAsync(
                user.Value.JwtToken, 
                request.Message, 
                repository.RepoOwner,
                repository.RepoName,
                issue.IssueNumber,
                ct);

        if (interfaceResult.IsFailure)
            return interfaceResult;

        return await _unitOfWork.SaveChangesAsync(new GiteaIssueDeleted(issue), ct);
    }
}

