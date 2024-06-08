using System.Text.Json.Serialization;
using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Project.Queries.GetAssignableHierarchies;

public record GetAssignableHierarchiesQuery(string ProjectId) : IQuery<GetAssignableHierarchiesResult>
{
    [JsonIgnore]
    public string UserId { get; set; } = "";
}

