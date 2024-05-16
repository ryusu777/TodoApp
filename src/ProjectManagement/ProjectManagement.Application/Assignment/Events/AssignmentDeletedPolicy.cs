using MassTransitContracts.ProjectManagement.Assignments;
using MassTransitContracts.Services;
using MediatR;
using ProjectManagement.Domain.Assignment.Events;

namespace ProjectManagement.Application.Assignment.Events;

public class AssignmentDeletedPolicy : INotificationHandler<AssignmentDeleted>
{
    private readonly IMassTransitService _messagingService;

    public AssignmentDeletedPolicy(IMassTransitService messagingService)
    {
        _messagingService = messagingService;
    }

    public Task Handle(AssignmentDeleted notification, CancellationToken cancellationToken)
    {
        return _messagingService.PublishEventAsync(new AssignmentDeletedMessage(
            notification.Assignment.Id.Value,
            notification.UserId.Value
        ), cancellationToken);
    }
}

