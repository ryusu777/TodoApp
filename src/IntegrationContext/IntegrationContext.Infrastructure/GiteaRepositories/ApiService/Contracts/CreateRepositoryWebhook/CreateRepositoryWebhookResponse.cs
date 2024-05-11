using System.Text.Json.Serialization;

namespace IntegrationContext.Infrastructure.GiteaRepositories.ApiService.Contracts.CreateRepositoryWebhook;

public class CreateRepositoryWebhookResponse
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("events")]
    public required ICollection<string> Events { get; set; }
    
    [JsonPropertyName("config")]
    public required CreateRepositoryWebhookRequest.Configuration Config { get; set; }

    [JsonPropertyName("active")]
    public required bool Active { get; set; }
}
