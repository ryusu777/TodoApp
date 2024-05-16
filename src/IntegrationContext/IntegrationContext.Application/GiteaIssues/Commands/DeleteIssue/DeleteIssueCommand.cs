using IntegrationContext.Application.Abstractions.Messaging;
using MassTransitContracts.ProjectManagement.Assignments;

namespace IntegrationContext.Application.GiteaIssues.Commands.DeleteIssue;

public record DeleteIssueCommand(AssignmentDeletedMessage Message) : ICommand;
