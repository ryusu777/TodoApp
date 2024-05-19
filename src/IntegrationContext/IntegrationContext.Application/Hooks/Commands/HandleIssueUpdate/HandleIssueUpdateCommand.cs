using IntegrationContext.Application.Abstractions.Messaging;

namespace IntegrationContext.Application.Hooks.Commands.HandleIssueUpdate;

public record HandleIssueUpdateCommand(
    int Id,
    string Title,
    string Body,
    ICollection<string> Assignees,
    string UpdatedAt,
    DateTime? DueDate
) : ICommand;
