using System.Text.Json.Serialization;

namespace IntegrationContext.Infrastructure.GiteaRepositories.ApiService.Contracts.CreateRepositoryWebhook;

public class CreateRepositoryWebhookResponse
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }
}
