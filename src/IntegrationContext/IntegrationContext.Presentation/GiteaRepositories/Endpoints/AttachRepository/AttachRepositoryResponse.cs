using IntegrationContext.Presentation.Core.Api;

namespace IntegrationContext.Presentation.GiteaRepositories.Endpoints.AttachRepositoryEndpoint;

public record AttachRepositoryResponse(
    string? ErrorCode,
    string? ErrorDescription
) : IApiResponse;
