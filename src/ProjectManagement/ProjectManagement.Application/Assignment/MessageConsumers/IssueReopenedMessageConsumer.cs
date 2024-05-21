using MassTransit;
using MassTransitContracts.Hooks.Issue;
using MediatR;
using ProjectManagement.Application.Assignment.Commands.ReopenAssignmentFromHook;

namespace ProjectManagement.Application.Assignment.MessageConsumers;

public class IssueReopenedMessageConsumer : IConsumer<IssueReopenedMessage>
{
    private readonly ISender _sender;

    public IssueReopenedMessageConsumer(ISender sender)
    {
        _sender = sender;
    }

    public Task Consume(ConsumeContext<IssueReopenedMessage> context)
    {
        return _sender
            .Send(new ReopenAssignmentFromHookCommand(
                context.Message.AssignmentId));
    }
}

