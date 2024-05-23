using MassTransitContracts.ProjectManagement.Assignments;
using MassTransitContracts.Services;
using MediatR;
using ProjectManagement.Domain.Assignment.Events;

namespace ProjectManagement.Application.Assignment.Events;

public class AssignmentRenewedPolicy : INotificationHandler<AssignmentRenewed>
{
    private readonly IMassTransitService _massTransitService;

    public AssignmentRenewedPolicy(IMassTransitService massTransitService)
    {
        _massTransitService = massTransitService;
    }

    public Task Handle(AssignmentRenewed notification, CancellationToken cancellationToken)
    {
        return _massTransitService.PublishEventAsync(new AssignmentRenewedMessage(notification.Id.Value, notification.UserId.Value), cancellationToken);
    }
}

