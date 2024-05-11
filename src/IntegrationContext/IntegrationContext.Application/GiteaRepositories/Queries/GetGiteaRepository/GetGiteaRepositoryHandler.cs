using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.Abstractions.Messaging;
using IntegrationContext.Application.Pagination.Models;
using IntegrationContext.Domain.Auth;
using IntegrationContext.Domain.Auth.ValueObjects;
using Library.Models;
using Microsoft.AspNetCore.Http;

namespace IntegrationContext.Application.GiteaRepositories.Queries.GetGiteaRepository;

public class GetGiteaRepositoryHandler : IQueryHandler<GetGiteaRepositoryQuery, GetGiteaRepositoryResult>
{
    private readonly IGiteaRepositoryService _giteaRepoService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    public GetGiteaRepositoryHandler(IGiteaRepositoryService giteaRepoService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
    {
        _giteaRepoService = giteaRepoService;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetGiteaRepositoryResult>> Handle(GetGiteaRepositoryQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor
            .HttpContext
            .User
            .Claims
            .FirstOrDefault(e => e.Type == "sub")
            ?.Value;

        if (userId is null)
            return Result.Failure<GetGiteaRepositoryResult>(AuthDomainError.GiteaUserNotAuthenticated);

        var result = await _giteaRepoService.GetGiteaRepositoriesAsync(
            UserId.Create(userId),
            request.SearchText,
            new Paging(request.ItemPerPage, request.Page),
            cancellationToken
        );

        if (result.IsFailure || result.Value is null)
            return Result.Failure<GetGiteaRepositoryResult>(result.Error);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(new GetGiteaRepositoryResult(result.Value));
    }
}

