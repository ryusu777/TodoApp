using System.Text.Json.Serialization;
using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.DeleteAssignment;

public record DeleteAssignmentCommand(Guid AssignmentId) : ICommand
{
    [JsonIgnore]
    public string UserId { get; set; } = "";
}
