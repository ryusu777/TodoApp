using System.Text.Json.Serialization;
using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Project.Commands.SyncProjectMembers;

public record SyncProjectMembersCommand(string ProjectId) : ICommand
{
    [JsonIgnore]
    public string UserId { get; set; }= "";
};
