using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.Abstractions.Messaging;
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
    private readonly IUnitOfWork _unitOfWork;

    public AttachRepositoryCommandHandler(IGiteaRepositoryService repoService, IUnitOfWork unitOfWork)
    {
        _repoService = repoService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AttachRepositoryCommand request, CancellationToken cancellationToken)
    {
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

