using System.Text.Json.Serialization;

namespace IntegrationContext.Infrastructure.GiteaIssues.ApiService.Contracts.CreateIssue;

public class CloseIssueRequest
{
    [JsonPropertyName("state")]
    public string State => "closed";
}
