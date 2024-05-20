using MassTransitContracts.Hooks.Issue;
using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.CompleteAssignmentFromHook;

public record CompleteAssignmentFromHookCommand(IssueClosedMessage Message) : ICommand;
