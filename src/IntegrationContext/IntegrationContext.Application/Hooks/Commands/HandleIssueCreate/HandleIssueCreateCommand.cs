using IntegrationContext.Application.Abstractions.Messaging;

namespace IntegrationContext.Application.Hooks.Commands.HandleIssueCreate;

public record HandleIssueCreateCommand(
    int Id,
    int IssueNumber,
    string Title,
    string Body,
    ICollection<string> Assignees,
    DateTime? DueDate,
    int GiteaRepositoryId
) : ICommand;
