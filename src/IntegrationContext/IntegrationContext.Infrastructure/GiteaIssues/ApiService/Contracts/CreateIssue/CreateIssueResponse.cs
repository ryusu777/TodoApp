using System.Text.Json.Serialization;

namespace IntegrationContext.Infrastructure.GiteaIssues.ApiService.Contracts.CreateIssue;

public class CreateIssueResponse
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("number")]
    public required int IssueNumber { get; set; }

    [JsonPropertyName("updated_at")]
    public required string UpdateAt { get; set; }
}
