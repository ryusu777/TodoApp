using IntegrationContext.Application.Abstractions.Messaging;
using IntegrationContext.Application.Pagination.Models;

namespace IntegrationContext.Application.GiteaRepositories.Queries.GetGiteaRepository;

public record GetGiteaRepositoryQuery(
    string SearchText,
    Paging Page
) : IQuery<GetGiteaRepositoryResult>;
