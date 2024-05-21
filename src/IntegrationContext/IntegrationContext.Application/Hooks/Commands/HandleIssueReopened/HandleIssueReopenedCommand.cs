using IntegrationContext.Application.Abstractions.Messaging;

namespace IntegrationContext.Application.Hooks.Commands.HandleIssueReopened;

public record HandleIssueReopenedCommand(int GiteaIssueId, string UpdateAt) : ICommand;
