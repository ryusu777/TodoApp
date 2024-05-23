using IntegrationContext.Application.GiteaIssues.Commands.ReopenIssue;
using MassTransit;
using MassTransitContracts.ProjectManagement.Assignments;
using MediatR;

namespace IntegrationContext.Application.GiteaIssues.MessageConsumers;

public class AssignmentRenewedConsumer : IConsumer<AssignmentRenewedMessage>
{
    private readonly ISender _sender;

    public AssignmentRenewedConsumer(ISender sender)
    {
        _sender = sender;
    }

    public Task Consume(ConsumeContext<AssignmentRenewedMessage> context)
    {
        return _sender.Send(new ReopenIssueCommand(context.Message), context.CancellationToken);
    }
}

