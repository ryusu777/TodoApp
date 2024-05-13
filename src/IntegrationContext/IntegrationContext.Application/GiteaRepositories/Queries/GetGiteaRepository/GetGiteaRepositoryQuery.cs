using IntegrationContext.Application.Abstractions.Messaging;

namespace IntegrationContext.Application.GiteaRepositories.Queries.GetGiteaRepository;

public record GetGiteaRepositoryQuery(
    string UserId,
    string? SearchText,
    int? Page,
    int? ItemPerPage
) : IQuery<GetGiteaRepositoryResult>;
