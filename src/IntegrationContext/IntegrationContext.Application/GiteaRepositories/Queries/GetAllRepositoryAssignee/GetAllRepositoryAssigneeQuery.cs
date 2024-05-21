using System.Text.Json.Serialization;
using IntegrationContext.Application.Abstractions.Messaging;

namespace IntegrationContext.Application.GiteaRepositories.Queries.GetAllRepositoryAsignee;

public record GetAllRepositoryAssigneeQuery(string ProjectId) : IQuery<GetAllRepositoryAsigneeResult>
{
    [JsonIgnore]
    public string UserId { get; set; } = "";
};
