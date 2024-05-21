using IntegrationContext.Application.GiteaRepositories.Dtos;

namespace IntegrationContext.Application.GiteaRepositories.Queries.GetAllRepositoryAsignee;

public class GetAllRepositoryAsigneeResult : List<GiteaAssigneeDto>
{
    public GetAllRepositoryAsigneeResult() { }
    public GetAllRepositoryAsigneeResult(IEnumerable<GiteaAssigneeDto> collection) : base(collection)
    {
    }
}
