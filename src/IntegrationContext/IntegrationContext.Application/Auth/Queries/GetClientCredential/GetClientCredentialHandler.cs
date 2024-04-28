using IntegrationContext.Application.Abstractions.Messaging;
using Library.Models;

namespace IntegrationContext.Application.Auth.Queries.GetClientCredential;

public class GetClientCredentialHandler : IQueryHandler<GetClientCredentialQuery, GetClientCredentialResult>
{
    private readonly IGiteaAuthenticationService _giteaAuthService;

    public GetClientCredentialHandler(IGiteaAuthenticationService giteaAuthService)
    {
        _giteaAuthService = giteaAuthService;
    }

    public async Task<Result<GetClientCredentialResult>> Handle(GetClientCredentialQuery request, CancellationToken cancellationToken)
    {
        var result = await _giteaAuthService.GetClientCredentials(cancellationToken);

        if (result.IsFailure || result.Value is null)
            return Result.Failure<GetClientCredentialResult>(result.Error);

        return Result.Success(new GetClientCredentialResult(
            result.Value.ClientId,
            result.Value.ClientSecret
        ));
    }
}

