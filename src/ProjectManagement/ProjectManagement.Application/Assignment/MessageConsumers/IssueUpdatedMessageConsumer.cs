using MassTransit;
using MassTransitContracts.Hooks.Issue;
using MediatR;
using ProjectManagement.Application.Assignment.Commands.UpdateAssignmentFromHook;

namespace ProjectManagement.Application.Assignment.MessageConsumers;

public class IssueUpdatedMessageConsumer : IConsumer<IssueUpdatedMessage>
{
    private readonly ISender _sender;

    public IssueUpdatedMessageConsumer(ISender sender)
    {
        _sender = sender;
    }

    public Task Consume(ConsumeContext<IssueUpdatedMessage> context)
    {
        return _sender
            .Send(new UpdateAssignmentFromHookCommand(
                context.Message));
    }
}

