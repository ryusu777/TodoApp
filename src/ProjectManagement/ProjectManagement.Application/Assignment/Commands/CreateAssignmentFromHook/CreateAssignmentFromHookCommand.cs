using MassTransitContracts.Hooks.Issue;
using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.CreateAssignmentFromHook;

public record CreateAssignmentFromHookCommand(
    IssueCreatedMessage Message
) : ICommand;
