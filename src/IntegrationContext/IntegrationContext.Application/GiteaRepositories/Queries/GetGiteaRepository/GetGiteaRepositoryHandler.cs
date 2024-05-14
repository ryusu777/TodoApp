using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.Abstractions.Messaging;
using IntegrationContext.Application.Auth;
using IntegrationContext.Application.Pagination.Models;
using IntegrationContext.Domain.Auth;
using IntegrationContext.Domain.Auth.ValueObjects;
using Library.Models;

namespace IntegrationContext.Application.GiteaRepositories.Queries.GetGiteaRepository;

public class GetGiteaRepositoryHandler : IQueryHandler<GetGiteaRepositoryQuery, GetGiteaRepositoryResult>
{
    private readonly IGiteaRepositoryService _giteaRepoService;
    private readonly IGiteaUserDomainService _userDomainService;
    private readonly IUnitOfWork _unitOfWork;

	public GetGiteaRepositoryHandler(IGiteaRepositoryService giteaRepoService, IUnitOfWork unitOfWork, IGiteaUserDomainService userDomainService)
	{
		_giteaRepoService = giteaRepoService;
		_unitOfWork = unitOfWork;
		_userDomainService = userDomainService;
	}

	public async Task<Result<GetGiteaRepositoryResult>> Handle(GetGiteaRepositoryQuery request, CancellationToken cancellationToken)
    {
        var user = await _userDomainService.GetOrRefreshJwt(UserId.Create(request.UserId), cancellationToken);

        if (user.IsFailure || user.Value is null || user.Value.JwtToken is null)
            return Result.Failure<GetGiteaRepositoryResult>(user.Error);

        var result = await _giteaRepoService.GetGiteaRepositoriesAsync(
            user.Value.JwtToken,
            request.SearchText,
            new Paging(request.ItemPerPage, request.Page),
            cancellationToken
        );

        if (result.IsFailure || result.Value is null)
            return Result.Failure<GetGiteaRepositoryResult>(result.Error);

        await _unitOfWork.SaveChangesAsync(user.Value.DomainEvents, cancellationToken);
        return Result.Success(new GetGiteaRepositoryResult(result.Value));
    }
}

