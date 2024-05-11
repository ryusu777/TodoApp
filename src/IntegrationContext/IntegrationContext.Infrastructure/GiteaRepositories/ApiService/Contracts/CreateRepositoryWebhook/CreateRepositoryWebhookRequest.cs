using System.Text.Json.Serialization;

namespace IntegrationContext.Infrastructure.GiteaRepositories.ApiService.Contracts.CreateRepositoryWebhook;

public class CreateRepositoryWebhookRequest
{
    public class Configuration
    {
        [JsonPropertyName("content_type")]
        public string ContentType { get; } = "json";

        [JsonPropertyName("url")]
        public required string Url { get; set; }
    }

    [JsonPropertyName("active")]
    public bool Active { get; } = true;

    [JsonPropertyName("authorization_header")]
    public required string AuthorizationHeader { get; set; }

    [JsonPropertyName("config")]
    public required Configuration Config { get; set; }

    [JsonPropertyName("events")]
    public required IEnumerable<string> Events { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; } = "gitea";
}


