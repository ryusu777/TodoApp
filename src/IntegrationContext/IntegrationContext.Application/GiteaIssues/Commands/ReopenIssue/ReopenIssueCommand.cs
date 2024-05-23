using IntegrationContext.Application.Abstractions.Messaging;
using MassTransitContracts.ProjectManagement.Assignments;

namespace IntegrationContext.Application.GiteaIssues.Commands.ReopenIssue;

public record ReopenIssueCommand(AssignmentRenewedMessage Message) : ICommand;
