using IntegrationContext.Application.Abstractions.Messaging;
using MassTransitContracts.ProjectManagement.Assignments;

namespace IntegrationContext.Application.GiteaIssues.Commands.CloseIssue;

public record CloseIssueCommand(AssignmentCompletedMessage Message) : ICommand;
