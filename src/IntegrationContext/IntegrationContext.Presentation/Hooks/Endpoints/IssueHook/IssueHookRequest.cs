using System.Text.Json.Serialization;

namespace IntegrationContext.Presentation.Hooks.Endpoints.IssueHook;

public class IssueHookRequest
{
    public static class IssueAction {
        public const string Opened = "opened";
        public const string Reopened = "reopened";
        public const string Updated = "updated";
        public const string Closed = "closed";
        public const string Assigned = "assigned";
        public const string Edited = "edited";
    }

    [JsonPropertyName("action")]
    public required string Action { get; set; }

    [JsonPropertyName("number")]
    public required int IssueNumber { get; set; }

    [JsonPropertyName("issue")]
    public required IssueDetail Issue { get; set; }

    [JsonPropertyName("repository")]
    public required RepositoryDetail Repository { get; set; }

    public class RepositoryDetail 
    {
        [JsonPropertyName("id")]
        public required int Id { get; set; }
    }

    public class IssueDetail {
        [JsonPropertyName("id")]
        public required int Id { get; set; }

        [JsonPropertyName("title")]
        public required string Title { get; set; }

        [JsonPropertyName("body")]
        public required string Body { get; set; }

        [JsonPropertyName("comments")]
        public required int CommentCount { get; set; }

        [JsonPropertyName("assignees")]
        public ICollection<IssueAssignee>? Assignees { get; set; }

        [JsonPropertyName("due_date")]
        public DateTime? DueDate { get; set; }
            
        [JsonPropertyName("updated_at")]
        public required string UpdatedAt { get; set; }

        public class IssueAssignee {
            [JsonPropertyName("login")]
            public required string Username { get; set; }
        }
    }
}
