using System.Text.Json.Serialization;

namespace IntegrationContext.Infrastructure.GiteaRepositories.ApiService.Contracts.GetRepositoryAssignees;

public class GetRepositoryAssigneesResponse
{
    [JsonPropertyName("username")]
    public required string Username { get; set; }
}
