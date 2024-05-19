using MassTransitContracts.Hooks.Issue;
using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.UpdateAssignmentFromHook;

public record UpdateAssignmentFromHookCommand(
    IssueUpdatedMessage Message) : ICommand;
