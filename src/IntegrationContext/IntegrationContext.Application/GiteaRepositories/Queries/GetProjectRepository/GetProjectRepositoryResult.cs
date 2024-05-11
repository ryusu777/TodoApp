using IntegrationContext.Application.GiteaRepositories.Dtos;

namespace IntegrationContext.Application.GiteaRepositories.Queries.GetProjectRepository;

public class GetProjectRepositoryResult : List<GiteaRepositoryDto>
{
    public GetProjectRepositoryResult(IEnumerable<GiteaRepositoryDto> collection) : base(collection)
    {
    }
}

