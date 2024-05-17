using MassTransit;
using MassTransitContracts.Hooks.Issue.IssueCreated;
using MediatR;
using ProjectManagement.Application.Assignment.Commands.CreateAssignmentFromHook;

namespace ProjectManagement.Application.Assignment.MessageConsumers;

public class IssueCreatedMessageConsumer : IConsumer<IssueCreatedMessage>
{
    private readonly ISender _sender;

    public IssueCreatedMessageConsumer(ISender sender)
    {
        _sender = sender;
    }

    public Task Consume(ConsumeContext<IssueCreatedMessage> context)
    {
        return _sender
            .Send(new CreateAssignmentFromHookCommand(
                context.Message));
    }
}

