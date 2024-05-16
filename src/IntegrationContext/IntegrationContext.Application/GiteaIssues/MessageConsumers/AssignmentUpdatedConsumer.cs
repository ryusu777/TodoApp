using IntegrationContext.Application.GiteaIssues.Commands.UpdateIssue;
using MassTransit;
using MassTransitContracts.ProjectManagement.Assignments;
using MediatR;

namespace IntegrationContext.Application.GiteaIssues.MessageConsumers;

public class AssignmentUpdatedConsumer : IConsumer<AssignmentUpdatedMessage>
{
    private readonly ISender _sender;

    public AssignmentUpdatedConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<AssignmentUpdatedMessage> context)
    {
        await _sender.Send(new UpdateIssueCommand(context.Message), context.CancellationToken);
    }
}
