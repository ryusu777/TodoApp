using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.CommandOutboxes;
using IntegrationContext.Application.CommandOutboxes.CommandHandler;
using IntegrationContext.Application.GiteaIssues;
using IntegrationContext.Application.GiteaRepositories;
using IntegrationContext.Domain.GiteaIssues.Events;
using IntegrationContext.Domain.GiteaIssues.ValueObjects;
using Library.Models;
using MassTransit;
using MassTransitContracts.Hooks.Issue;
using MassTransitContracts.Services;

namespace IntegrationContext.Application.Hooks.Commands.HandleIssueUpdate;

public class HandleIssueUpdateCommandHandler : OutboxedCommandHandler<HandleIssueUpdateCommand>
{
    private IRequestClient<IssueUpdatedMessage> _client;
    private IMassTransitService _massTransitService;
    private IGiteaRepositoryRepository _repoRepository;
    private IGiteaIssueRepository _issueRepository;

    public HandleIssueUpdateCommandHandler(
        IUnitOfWork unitOfWork,
        ICommandOutboxDomainService outboxDomainService,
        IRequestClient<IssueUpdatedMessage> client,
        IMassTransitService massTransitService,
        IGiteaRepositoryRepository repoRepository,
        IGiteaIssueRepository issueRepository) : base(unitOfWork, outboxDomainService)
    {
        _client = client;
        _massTransitService = massTransitService;
        _repoRepository = repoRepository;
        _issueRepository = issueRepository;
    }

    protected override async Task<Result> HandleInternal(HandleIssueUpdateCommand request, CancellationToken cancellationToken)
    {
        var issueResult = await _issueRepository.GetIssueByIdAsync(GiteaIssueId.Create(request.Id), cancellationToken);

        if (issueResult.IsFailure || issueResult.Value is null)
            return issueResult;

        if (issueResult.Value.UpdatedAt == request.UpdatedAt)
            return Result.Success();

        issueResult.Value.Update(request.UpdatedAt);

        var persistResult = await _unitOfWork.SaveChangesAsync(new GiteaIssueUpdated(issueResult.Value), cancellationToken);

        if (persistResult.IsFailure)
            return persistResult;

        await _massTransitService.PublishEventAsync(
            new IssueUpdatedMessage(
                issueResult.Value.AssignmentId.Value,
                request.Title,
                request.Body,
                request.Assignees,
                request.DueDate
            ), cancellationToken
        );

        return Result.Success();
    }
}

