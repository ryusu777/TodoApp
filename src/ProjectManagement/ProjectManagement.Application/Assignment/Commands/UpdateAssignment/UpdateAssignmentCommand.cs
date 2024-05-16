using System.Text.Json.Serialization;
using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.UpdateAssignments;

public record UpdateAssignmentCommand(
    Guid AssignmentId, 
    string Title, 
    string Description,
    string[] Assignees,
    Guid? SubdomainId = null,
    Guid? PhaseId = null,
    string? Reviewer = null
) : ICommand
{
    [JsonIgnore]
    public string UserId { get; set; } = "";
};
