using MassTransitContracts.ProjectManagement.Assignments;
using MassTransitContracts.Services;
using MediatR;
using ProjectManagement.Domain.Assignment.Events;

namespace ProjectManagement.Application.Assignment.Events;

public class AssignmentUpdatedPolicy : INotificationHandler<AssignmentUpdated>
{
    private readonly IMassTransitService _messagingService;

    public AssignmentUpdatedPolicy(IMassTransitService messagingService)
    {
        _messagingService = messagingService;
    }

    public Task Handle(AssignmentUpdated notification, CancellationToken cancellationToken)
    {
        return _messagingService.PublishEventAsync(new AssignmentUpdatedMessage(
            notification.UserId.Value,
            notification.Assignment.Id.Value,
            notification.Assignment.Title,
            notification.Assignment.Description,
            notification.Assignment.Assignees.Select(e => e.Value).ToList(),
            notification.Assignment.Deadline
        ), cancellationToken);
    }
}

