using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.Abstractions.Messaging;
using IntegrationContext.Application.Auth;
using IntegrationContext.Application.GiteaRepositories.Queries.GetGiteaRepository;
using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Domain.GiteaRepositories;
using IntegrationContext.Domain.GiteaRepositories.Entities;
using IntegrationContext.Domain.GiteaRepositories.Events;
using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using Library.Models;

namespace IntegrationContext.Application.GiteaRepositories.Commands.AttachRepository;

public class AttachRepositoryCommandHandler : ICommandHandler<AttachRepositoryCommand>
{
    private readonly IGiteaRepositoryService _repoService;
    private readonly IGiteaUserDomainService _userDomainService;
    private readonly IGiteaRepositoryRepository _repoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AttachRepositoryCommandHandler(
        IGiteaRepositoryService repoService, 
        IUnitOfWork unitOfWork, 
        IGiteaUserDomainService userDomainService, 
        IGiteaRepositoryRepository repoRepository
    )
    {
        _unitOfWork = unitOfWork;
        _repoService = repoService;
        _userDomainService = userDomainService;
        _repoRepository = repoRepository;
    }

    public async Task<Result> Handle(AttachRepositoryCommand request, CancellationToken cancellationToken)
    {
        var user = await _userDomainService.GetOrRefreshJwt(UserId.Create(request.UserId), cancellationToken);

        if (user.IsFailure || user.Value is null || user.Value.JwtToken is null)
            return Result.Failure<GetGiteaRepositoryResult>(user.Error);

        var foundRepo = await _repoRepository
            .GetProjectRepositoriesAsync(ProjectId.Create(request.ProjectId), cancellationToken);

        if (foundRepo.Value is not null &&
            foundRepo.Value.Any(e => e.RepoOwner == request.RepoOwner && e.RepoName == request.RepoName))
            return Result.Failure<GetGiteaRepositoryResult>(GiteaRepositoryDomainError.GiteaRepositoryAlreadyAttached);

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

