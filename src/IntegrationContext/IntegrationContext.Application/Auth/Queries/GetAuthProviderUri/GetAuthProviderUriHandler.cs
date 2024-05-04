using IntegrationContext.Application.Abstractions.Messaging;
using IntegrationContext.Application.Auth.Models;
using Library.Models;
using MassTransitContracts.GetAuthProviderUri;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace IntegrationContext.Application.Auth.Queries.GetAuthProviderUri;

public class GetAuthProviderUriHandler 
    : IQueryHandler<GetAuthProviderUriQuery, 
        GetAuthProviderUriResponse>
{
    private readonly string _giteaUrl;
    private readonly GiteaClientCredentials _credentials;
    private readonly string _clientUrl;
    private string BaseUrl => $"{_clientUrl}/authorize-gitea";

    public GetAuthProviderUriHandler(IConfiguration config, IOptions<GiteaClientCredentials> credentials)
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

    public Task<Result<GetAuthProviderUriResponse>> Handle(GetAuthProviderUriQuery request, CancellationToken cancellationToken)
    {
        var uriBuilder = new UriBuilder(_giteaUrl);
        uriBuilder.Path = "/login/oauth/authorize";
        uriBuilder.Query = 
            "client_id=" + _credentials.ClientId + "&" +
            "redirect_uri=" + BaseUrl + "&" +
            "response_type=code" + "&" +
            "state=" + request.State
            ;

        return Task.FromResult(Result.Success(new GetAuthProviderUriResponse(uriBuilder.Uri)));
    }
}

