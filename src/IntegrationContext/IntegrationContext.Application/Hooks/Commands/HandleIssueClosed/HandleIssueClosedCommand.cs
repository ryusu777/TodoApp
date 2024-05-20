using IntegrationContext.Application.Abstractions.Messaging;

namespace IntegrationContext.Application.Hooks.Commands.HandleIssueClosed;

public record HandleIssueClosedCommand(
    int GiteaIssueId,
    string UpdatedAt
) : ICommand;
