using System.Text.Json.Serialization;

namespace IntegrationContext.Infrastructure.GiteaRepositories.ApiService.Contracts.GetRepositoryActivities;

public class GetRepositoryActivitiesResponse
{
    [JsonPropertyName("content")]
    public required string Content { get; set; }

    [JsonPropertyName("op_type")]
    public required string Operation { get; set; }

    [JsonPropertyName("created")]
    public required string Created { get; set; }

    [JsonPropertyName("repo_id")]
    public required int RepoId { get; set; }
}
