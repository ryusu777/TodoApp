namespace IntegrationContext.Application.Auth.Models;

public class GiteaClientCredentials
{
    public const string OptionSection = "GiteaCredentials";

    public string ClientId { get; set; } = String.Empty;
    public string ClientSecret { get; set; } = String.Empty;
}
