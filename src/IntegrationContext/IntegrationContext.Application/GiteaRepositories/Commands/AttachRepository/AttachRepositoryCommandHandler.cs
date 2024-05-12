using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.Abstractions.Messaging;
using IntegrationContext.Application.Auth;
using IntegrationContext.Application.GiteaRepositories.Queries.GetGiteaRepository;
using IntegrationContext.Domain.Auth;
using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Domain.GiteaRepositories;
using IntegrationContext.Domain.GiteaRepositories.Entities;
using IntegrationContext.Domain.GiteaRepositories.Events;
using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using Library.Models;
using Microsoft.AspNetCore.Http;

namespace IntegrationContext.Application.GiteaRepositories.Commands.AttachRepository;

public class AttachRepositoryCommandHandler : ICommandHandler<AttachRepositoryCommand>
{
    private readonly IGiteaRepositoryService _repoService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IGiteaUserDomainService _userDomainService;

	public AttachRepositoryCommandHandler(IGiteaRepositoryService repoService, IUnitOfWork unitOfWork, IGiteaUserDomainService userDomainService, IHttpContextAccessor httpContextAccessor)
	{
		_repoService = repoService;
		_unitOfWork = unitOfWork;
		_userDomainService = userDomainService;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<Result> Handle(AttachRepositoryCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor
            .HttpContext
            .User
            .Claims
            .FirstOrDefault(e => e.Type == "sub")
            ?.Value;

        if (userId is null)
            return Result.Failure<GetGiteaRepositoryResult>(AuthDomainError.GiteaUserNotAuthenticated);

        var user = await _userDomainService.GetOrRefreshJwt(UserId.Create(userId), cancellationToken);

        if (user.IsFailure || user.Value is null || user.Value.JwtToken is null)
            return Result.Failure<GetGiteaRepositoryResult>(user.Error);

        ProjectId projectId = ProjectId.Create(request.ProjectId);

        var repository = await _repoService
            .GetGiteaRepositoryByOwnerAsync(
                UserId.Create(request.RepoOwner),
                request.RepoName,
                cancellationToken
            );

        if (repository.IsFailure || repository.Value is null)
            return repository.Error;

        var createHookResult = await _repoService
            .CreateRepositoryHookAsync(
				user.Value.JwtToken,
                projectId,
                request.RepoOwner,
                request.RepoName,
                cancellationToken
            );

        if (createHookResult.IsFailure || createHookResult.Value is null)
            return createHookResult.Error;

        var entity = GiteaRepository.Create(
            GiteaRepositoryId.Create(repository.Value.Id),
            projectId,
            UserId.Create(request.RepoOwner),
            request.RepoName,
            new RepositoryHook[] 
            {
                createHookResult.Value
            }
        );

        entity.RaiseDomainEvent(new GiteaRepositoryCreated(entity));

        return await _unitOfWork.SaveChangesAsync(entity.DomainEvents, cancellationToken);
    }
}

