using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.CommandOutboxes;
using IntegrationContext.Application.CommandOutboxes.CommandHandler;
using IntegrationContext.Application.GiteaIssues;
using IntegrationContext.Domain.GiteaIssues.ValueObjects;
using Library.Models;
using MassTransitContracts.Hooks.Issue;
using MassTransitContracts.Services;

namespace IntegrationContext.Application.Hooks.Commands.HandleIssueClosed;

public class HandleIssueClosedCommandHandler : OutboxedCommandHandler<HandleIssueClosedCommand>
{
    private readonly IMassTransitService _massTransitService;
    private readonly IGiteaIssueRepository _issueRepository;

    public HandleIssueClosedCommandHandler(
        IUnitOfWork unitOfWork, 
        ICommandOutboxDomainService outboxDomainService, 
        IMassTransitService massTransitService, 
        IGiteaIssueRepository issueRepository) : base(unitOfWork, outboxDomainService)
    {
        _massTransitService = massTransitService;
        _issueRepository = issueRepository;
    }

    protected override async Task<Result> HandleInternal(HandleIssueClosedCommand request, CancellationToken cancellationToken)
    {
        var issueResult = await _issueRepository.GetIssueByIdAsync(GiteaIssueId.Create(request.GiteaIssueId), cancellationToken);

        if (issueResult.IsFailure || issueResult.Value is null)
            return issueResult;

        if (issueResult.Value.UpdatedAt == request.UpdatedAt)
            return Result.Success();

        await _massTransitService.PublishEventAsync(new IssueClosedMessage(issueResult.Value.AssignmentId.Value), cancellationToken);

        issueResult.Value.Update(request.UpdatedAt);

        return await _unitOfWork.SaveChangesAsync(issueResult.Value.DomainEvents, cancellationToken);
    }
}

