using IntegrationContext.Application.Abstractions.Messaging;

namespace IntegrationContext.Application.GiteaRepositories.Queries.GetProjectRepository;

public record GetProjectRepositoryQuery(string ProjectId) : IQuery<GetProjectRepositoryResult>;
