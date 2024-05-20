using MassTransit;
using MassTransitContracts.Hooks.Issue;
using MediatR;
using ProjectManagement.Application.Assignment.Commands.CompleteAssignmentFromHook;

namespace ProjectManagement.Application.Assignment.MessageConsumers;

public class IssueClosedMessageConsumer : IConsumer<IssueClosedMessage>
{
    private readonly ISender _sender;

    public IssueClosedMessageConsumer(ISender sender)
    {
        _sender = sender;
    }

    public Task Consume(ConsumeContext<IssueClosedMessage> context)
    {
        return _sender
            .Send(new CompleteAssignmentFromHookCommand(
                context.Message));
    }
}

