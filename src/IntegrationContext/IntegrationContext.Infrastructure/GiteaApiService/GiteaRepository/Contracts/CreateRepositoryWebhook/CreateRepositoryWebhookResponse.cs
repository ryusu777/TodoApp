using System.Text.Json.Serialization;

namespace IntegrationContext.Infrastructure.GiteaApiService.GiteaRepository.Contracts.CreateRepositoryWebhook;

public class CreateRepositoryWebhookResponse
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }
}
