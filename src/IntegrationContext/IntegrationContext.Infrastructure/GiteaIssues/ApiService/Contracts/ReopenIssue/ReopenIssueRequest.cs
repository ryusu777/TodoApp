using System.Text.Json.Serialization;

namespace IntegrationContext.Infrastructure.GiteaIssues.ApiService.Contracts.CreateIssue;

public class ReopenIssueRequest
{
    [JsonPropertyName("state")]
    public string State => "open";
}
