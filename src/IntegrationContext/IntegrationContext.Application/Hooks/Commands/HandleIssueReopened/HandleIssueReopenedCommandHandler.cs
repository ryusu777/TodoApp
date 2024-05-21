using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.CommandOutboxes;
using IntegrationContext.Application.CommandOutboxes.CommandHandler;
using IntegrationContext.Application.GiteaIssues;
using IntegrationContext.Domain.GiteaIssues.ValueObjects;
using Library.Models;
using MassTransitContracts.Hooks.Issue;
using MassTransitContracts.Services;

namespace IntegrationContext.Application.Hooks.Commands.HandleIssueReopened;

public class HandleIssueReopenedCommandHandler : OutboxedCommandHandler<HandleIssueReopenedCommand>
{
    private readonly IMassTransitService _massTransitService;
    private readonly IGiteaIssueRepository _issueRepository;

    public HandleIssueReopenedCommandHandler(
        IUnitOfWork unitOfWork,
        ICommandOutboxDomainService outboxDomainService,
        IMassTransitService massTransitService,
        IGiteaIssueRepository issueRepository) : base(unitOfWork, outboxDomainService)
    {
        _massTransitService = massTransitService;
        _issueRepository = issueRepository;
    }

    protected override async Task<Result> HandleInternal(HandleIssueReopenedCommand request, CancellationToken ct)
    {
        var issueResult = await _issueRepository.GetIssueByIdAsync(GiteaIssueId.Create(request.GiteaIssueId), ct);

        if (issueResult.IsFailure || issueResult.Value is null)
            return issueResult;

        await _massTransitService
            .PublishEventAsync(new IssueReopenedMessage(issueResult.Value.AssignmentId.Value), ct);

        issueResult.Value.Update(request.UpdateAt);
        return await _unitOfWork.SaveChangesAsync(issueResult.Value.DomainEvents, ct);
    }
}
