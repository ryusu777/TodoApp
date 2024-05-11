using IntegrationContext.Application.GiteaRepositories.Dtos;

namespace IntegrationContext.Application.GiteaRepositories.Queries.GetGiteaRepository;

public class GetGiteaRepositoryResult : List<GiteaRepositoryDto>
{
    public GetGiteaRepositoryResult(IEnumerable<GiteaRepositoryDto> collection) : base(collection)
    {
    }
}
