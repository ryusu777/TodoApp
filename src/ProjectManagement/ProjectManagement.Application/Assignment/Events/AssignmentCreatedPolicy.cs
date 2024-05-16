using MassTransitContracts.ProjectManagement.Assignments;
using MassTransitContracts.Services;
using MediatR;
using ProjectManagement.Domain.Assignment.Events;

namespace ProjectManagement.Application.Assignment.Events;

public class AssignmentCreatedPolicy : INotificationHandler<AssignmentCreated>
{
    private readonly IMassTransitService _messagingService;

    public AssignmentCreatedPolicy(IMassTransitService messagingService)
    {
        _messagingService = messagingService;
    }

    public Task Handle(AssignmentCreated notification, CancellationToken cancellationToken)
    {
        return _messagingService
            .PublishEventAsync(new AssignmentCreatedMessage(
                notification.UserId.Value,
                notification.Assignment.Id.Value,
                notification.Assignment.Title,
                notification.Assignment.Description,
                notification.Assignment.ProjectId.Value,
                notification.Assignment.Assignees.Select(e => e.Value).ToList(),
                notification.Assignment.Deadline,
                notification.GiteaRepositoryId
            ), cancellationToken);
    }
}

