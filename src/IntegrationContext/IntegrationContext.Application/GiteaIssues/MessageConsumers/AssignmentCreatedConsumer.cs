using IntegrationContext.Application.GiteaIssues.Commands.CreateIssue;
using MassTransit;
using MassTransitContracts.ProjectManagement.Assignments;
using MediatR;

namespace IntegrationContext.Application.GiteaIssues.MessageConsumers;

public class AssignmentCreatedConsumer : IConsumer<AssignmentCreatedMessage>
{
    private readonly ISender _sender;

    public AssignmentCreatedConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<AssignmentCreatedMessage> context)
    {
        await _sender.Send(new CreateIssueCommand(context.Message), context.CancellationToken);
    }
}
