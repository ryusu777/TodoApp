using IntegrationContext.Application.Auth.Models;
using MassTransit;
using MassTransitContracts.GetAuthProviderUri;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace IntegrationContext.Application.Auth.Messaging.GetAuthProviderUri;

public class GetAuthProviderUriConsumer : IConsumer<GetAuthProviderUriRequest>
{
    private readonly string _giteaUrl;
    private readonly GiteaClientCredentials _credentials;
    private readonly string _clientUrl;
    private string BaseUrl => $"{_clientUrl}/authorize-gitea";

    public GetAuthProviderUriConsumer(IConfiguration config, IOptions<GiteaClientCredentials> credentials)
    {
        var url = config["GiteaUrl"];
        if (url is null)
            throw new Exception("Gitea url is not found, please set Gitea Url in configuration");

        var clientUrl = config["ClientUrl"];
        if (clientUrl is null)
            throw new Exception("Client url is not found, please set Client Url in configuration");

        _clientUrl = clientUrl;
        _giteaUrl = url;
        _credentials = credentials.Value;
    }

    public async Task Consume(ConsumeContext<GetAuthProviderUriRequest> context)
    {
        var uriBuilder = new UriBuilder(_giteaUrl);
        uriBuilder.Path = "/login/oauth/authorize";
        uriBuilder.Query = 
            "client_id=" + _credentials.ClientId + "&" +
            "redirect_uri=" + BaseUrl + "&" +
            "response_type=code" + "&" +
            "state=" + context.Message.State
            ;

        await context.RespondAsync(new GetAuthProviderUriResponse(uriBuilder.Uri));
    }
}

