using IntegrationContext.Application.GiteaIssues.Commands.DeleteIssue;
using MassTransit;
using MassTransitContracts.ProjectManagement.Assignments;
using MediatR;

namespace IntegrationContext.Application.GiteaIssues.MessageConsumers;

public class AssignmentDeletedConsumer : IConsumer<AssignmentDeletedMessage>
{
    private readonly ISender _sender;

    public AssignmentDeletedConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<AssignmentDeletedMessage> context)
    {
        await _sender.Send(new DeleteIssueCommand(context.Message), context.CancellationToken);
    }
}
