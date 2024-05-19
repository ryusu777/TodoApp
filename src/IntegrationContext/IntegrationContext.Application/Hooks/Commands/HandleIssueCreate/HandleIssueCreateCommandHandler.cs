using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.CommandOutboxes;
using IntegrationContext.Application.CommandOutboxes.CommandHandler;
using IntegrationContext.Application.GiteaIssues;
using IntegrationContext.Application.GiteaRepositories;
using IntegrationContext.Domain.GiteaIssues;
using IntegrationContext.Domain.GiteaIssues.Events;
using IntegrationContext.Domain.GiteaIssues.ValueObjects;
using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using IntegrationContext.Domain.Hooks;
using Library.Models;
using MassTransit;
using MassTransitContracts.Hooks.Issue;
using MassTransitContracts.Services;

namespace IntegrationContext.Application.Hooks.Commands.HandleIssueCreate;

public class HandleIssueCreateCommandHandler : OutboxedCommandHandler<HandleIssueCreateCommand>
{
    private IRequestClient<IssueCreatedMessage> _client;
    private IMassTransitService _massTransitService;
    private IGiteaRepositoryRepository _repoRepository;
    private IGiteaIssueRepository _issueRepository;

    public HandleIssueCreateCommandHandler(
        IUnitOfWork unitOfWork,
        ICommandOutboxDomainService outboxDomainService,
        IRequestClient<IssueCreatedMessage> client,
        IMassTransitService massTransitService,
        IGiteaRepositoryRepository repoRepository,
        IGiteaIssueRepository issueRepository) : base(unitOfWork, outboxDomainService)
    {
        _client = client;
        _massTransitService = massTransitService;
        _repoRepository = repoRepository;
        _issueRepository = issueRepository;
    }

    protected override async Task<Result> HandleInternal(HandleIssueCreateCommand request, CancellationToken cancellationToken)
    {
        if (await _issueRepository.IssueExistsAsync(GiteaIssueId.Create(request.Id), cancellationToken))
            return Result.Success();

        AssignmentId assignmentId = AssignmentId.CreateUnique();

        var repository = await _repoRepository
            .GetProjectRepositoryByIdAsync(GiteaRepositoryId.Create(request.GiteaRepositoryId), cancellationToken);

        if (repository.IsFailure || repository.Value is null)
            return repository.Error;

        try
        {
            var issue = GiteaIssue.Create(
                GiteaIssueId.Create(request.Id),
                IssueNumber.Create(request.IssueNumber),
                assignmentId,
                GiteaRepositoryId.Create(request.GiteaRepositoryId));

            await _unitOfWork.SaveChangesAsync(new GiteaIssueCreated(issue), cancellationToken);
        }
        catch (Exception e)
        {
            return HooksDomainError.FailedToCreateAssignment(e.Message);
        }

        await _massTransitService.PublishEventAsync(
            new IssueCreatedMessage(
                assignmentId.Value,
                repository.Value.ProjectId.Value,
                request.Title,
                request.Body,
                request.Assignees,
                request.DueDate
            ), cancellationToken
        );

        return Result.Success();
    }
}

