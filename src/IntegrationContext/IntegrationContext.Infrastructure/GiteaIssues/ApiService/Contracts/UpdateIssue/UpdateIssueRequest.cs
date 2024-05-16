using System.Text.Json.Serialization;

namespace IntegrationContext.Infrastructure.GiteaIssues.ApiService.Contracts.UpdateIssue;

public class UpdateIssueRequest
{
    [JsonPropertyName("assignees")]
    public required ICollection<string> Assignees { get; set; }

    [JsonPropertyName("body")]
    public string? Body { get; set; }

    [JsonPropertyName("due_date")]
    public DateTime? DueDate { get; set; }

    [JsonPropertyName("title")]
    public required string Title { get; set; }
}
