using IntegrationContext.Application.Auth.Models;
using MassTransit;
using MassTransitContracts.GetAuthProviderUri;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace IntegrationContext.Application.Auth.Messaging.GetAuthProviderUri;

public class GetAuthProviderUriConsumer : IConsumer<GetAuthProviderUriRequest>
{
    private readonly string _giteaUrl;
    private readonly GiteaClientCredentials _credentials;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private string BaseUrl => $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/authorize-gitea";

    public GetAuthProviderUriConsumer(IConfiguration config, IOptions<GiteaClientCredentials> credentials, IHttpContextAccessor httpContextAccessor)
    {
        var url = config["GiteaUrl"];
        if (url is null)
            throw new Exception("Gitea url is not found, please set Gitea Url in configuration");
        _giteaUrl = url;
        _credentials = credentials.Value;
        _httpContextAccessor = httpContextAccessor;
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

