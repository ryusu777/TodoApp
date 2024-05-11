using IntegrationContext.Application.Abstractions.Messaging;
using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using Library.Models;

namespace IntegrationContext.Application.GiteaRepositories.Queries.GetProjectRepository;

public class GetProjectRepositoryHandler : IQueryHandler<GetProjectRepositoryQuery, GetProjectRepositoryResult>
{
    private readonly IGiteaRepositoryRepository _repo;

    public GetProjectRepositoryHandler(IGiteaRepositoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<GetProjectRepositoryResult>> Handle(GetProjectRepositoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _repo
            .GetProjectRepositoriesAsync(
                ProjectId.Create(request.ProjectId), cancellationToken);

        if (result.IsFailure || result.Value is null)
            return Result.Failure<GetProjectRepositoryResult>(result.Error);

        return Result.Success(new GetProjectRepositoryResult(result.Value));
    }
}

