using IntegrationContext.Application.GiteaIssues.Commands.CloseIssue;
using MassTransit;
using MassTransitContracts.ProjectManagement.Assignments;
using MediatR;

namespace IntegrationContext.Application.GiteaIssues.MessageConsumers;

public class AssignmentCompletedConsumer : IConsumer<AssignmentCompletedMessage>
{
    private readonly ISender _sender;

    public AssignmentCompletedConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<AssignmentCompletedMessage> context)
    {
        await _sender.Send(new CloseIssueCommand(context.Message), context.CancellationToken);
    }
}
