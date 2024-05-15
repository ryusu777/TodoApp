using MassTransit;
using MassTransitContracts.ProjectManagement.Assignments;
using MediatR;
using ProjectManagement.Domain.Assignment.Events;

namespace ProjectManagement.Application.Assignment.Events;

public class AssignmentCreatedPolicy : INotificationHandler<AssignmentCreated>
{
    private readonly IBus _messageBus;

    public AssignmentCreatedPolicy(IBus messageBus)
    {
        _messageBus = messageBus;
    }

    public Task Handle(AssignmentCreated notification, CancellationToken cancellationToken)
    {
        return _messageBus
            .Publish(new AssignmentCreatedMessage(
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

