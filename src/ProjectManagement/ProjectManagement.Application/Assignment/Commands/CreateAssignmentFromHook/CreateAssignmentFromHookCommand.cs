using MassTransitContracts.Hooks.Issue.IssueCreated;
using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.CreateAssignmentFromHook;

public record CreateAssignmentFromHookCommand(
    IssueCreatedMessage Message
) : ICommand;
