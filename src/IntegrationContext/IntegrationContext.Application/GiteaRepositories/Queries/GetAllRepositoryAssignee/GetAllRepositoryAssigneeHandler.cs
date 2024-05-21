using IntegrationContext.Application.Abstractions.Messaging;
using IntegrationContext.Application.Auth;
using IntegrationContext.Application.GiteaRepositories.Dtos;
using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using Library.Models;

namespace IntegrationContext.Application.GiteaRepositories.Queries.GetAllRepositoryAsignee;

public class GetAllRepositoryAssigneeHandler : IQueryHandler<GetAllRepositoryAssigneeQuery, GetAllRepositoryAsigneeResult>
{
    private readonly IGiteaRepositoryRepository _repoRepository;
    private readonly IGiteaRepositoryApiService _repoApiService;
    private readonly IGiteaUserDomainService _userDomainService;

    public GetAllRepositoryAssigneeHandler(IGiteaRepositoryRepository repoRepository, IGiteaRepositoryApiService repoApiService, IGiteaUserDomainService userDomainService)
    {
        _repoRepository = repoRepository;
        _repoApiService = repoApiService;
        _userDomainService = userDomainService;
    }

    public async Task<Result<GetAllRepositoryAsigneeResult>> Handle(GetAllRepositoryAssigneeQuery request, CancellationToken cancellationToken)
    {
        var user = await _userDomainService.GetOrRefreshJwt(UserId.Create(request.UserId), cancellationToken);

        if (user.IsFailure || user.Value is null || user.Value.JwtToken is null)
            return Result.Failure<GetAllRepositoryAsigneeResult>(user.Error);

        var reposResult = await _repoRepository.GetProjectRepositoriesAsync(ProjectId.Create(request.ProjectId), cancellationToken);
        if (reposResult.IsFailure || reposResult.Value is null)
            return Result.Failure<GetAllRepositoryAsigneeResult>(reposResult.Error);

        var assigneesSet = new HashSet<GiteaAssigneeDto>();

        foreach (var repo in reposResult.Value)
        {
            var repoAssignees = await _repoApiService
                .GetGiteaRepositoryAssigneesAsync(
                    user.Value.JwtToken,
                    UserId.Create(repo.RepoOwner),
                    repo.RepoName,
                    cancellationToken);

            if (repoAssignees.IsFailure || repoAssignees.Value is null)
                return Result.Failure<GetAllRepositoryAsigneeResult>(repoAssignees.Error);

            foreach (var assignee in repoAssignees.Value)
                assigneesSet.Add(assignee);
        }

        return Result
            .Success(new GetAllRepositoryAsigneeResult(assigneesSet.ToList()));
    }
}

