using IntegrationContext.Application.Abstractions.Messaging;
using MassTransitContracts.ProjectManagement.Assignments;

namespace IntegrationContext.Application.GiteaIssues.Commands.CreateIssue;

public record CreateIssueCommand(AssignmentCreatedMessage Message) : ICommand;
