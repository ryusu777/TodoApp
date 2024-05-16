using IntegrationContext.Application.Abstractions.Messaging;
using MassTransitContracts.ProjectManagement.Assignments;

namespace IntegrationContext.Application.GiteaIssues.Commands.UpdateIssue;

public record UpdateIssueCommand(
    AssignmentUpdatedMessage Message
) : ICommand;
