using System.Text.Json.Serialization;
using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.ChangeAssignmentStatus;

public record ChangeAssignmentStatusCommand(
    Guid AssignmentId,
    string AssignmentStatus
) : ICommand
{
    [JsonIgnore]
    public string UserId { get; set; } = "";
};

