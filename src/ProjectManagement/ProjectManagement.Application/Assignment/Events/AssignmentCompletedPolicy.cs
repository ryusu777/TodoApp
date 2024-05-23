using MassTransitContracts.ProjectManagement.Assignments;
using MassTransitContracts.Services;
using MediatR;
using ProjectManagement.Domain.Assignment.Events;

namespace ProjectManagement.Application.Assignment.Events;

public class AssignmentCompletedPolicy : INotificationHandler<AssignmentCompleted>
{
    private readonly IMassTransitService _messagingService;

    public AssignmentCompletedPolicy(IMassTransitService messagingService)
    {
        _messagingService = messagingService;
    }

    public Task Handle(AssignmentCompleted notification, CancellationToken cancellationToken)
    {
        return _messagingService
            .PublishEventAsync(new AssignmentCompletedMessage(notification.Id.Value), cancellationToken);
    }
}

