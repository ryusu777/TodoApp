using IntegrationContext.Application.Abstractions.Messaging;
using Library.Models;

namespace IntegrationContext.Application.GiteaRepositories.Queries.GetGiteaRepository;

public class GetGiteaRepositoryHandler : IQueryHandler<GetGiteaRepositoryQuery, GetGiteaRepositoryResult>
{
    private readonly IGiteaRepositoryService _giteaRepoService;

    public GetGiteaRepositoryHandler(IGiteaRepositoryService giteaRepoService)
    {
        _giteaRepoService = giteaRepoService;
    }

    public async Task<Result<GetGiteaRepositoryResult>> Handle(GetGiteaRepositoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _giteaRepoService.GetGiteaRepositoriesAsync(
            request.SearchText,
            request.Page,
            cancellationToken
        );

        if (result.IsFailure || result.Value is null)
            return Result.Failure<GetGiteaRepositoryResult>(result.Error);

        return Result.Success(new GetGiteaRepositoryResult(result.Value));
    }
}

