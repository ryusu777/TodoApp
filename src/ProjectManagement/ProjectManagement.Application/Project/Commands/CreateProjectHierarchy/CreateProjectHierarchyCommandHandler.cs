using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.Entities;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Commands.CreateProjectHierarchy;

public class CreateProjectHierarchyCommandHandler : ICommandHandler<CreateProjectHierarchyCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectHierarchyCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateProjectHierarchyCommand request, CancellationToken cancellationToken)
    {
        var projectResult = await _projectRepository.GetProjectById(ProjectId.Create(request.ProjectId), cancellationToken);

        if (projectResult.IsFailure || projectResult.Value is null)
        {
            return projectResult;
        }

        var superiorHierarchyId = request.SuperiorHierarchyId.HasValue ? HierarchyId.Create(request.SuperiorHierarchyId!.Value) : null;
        var memberUsernames = request.MemberUsernames.Select(x => UserId.Create(x)).ToList();

        var hierarchy = Hierarchy.Create(
            request.Name,
            superiorHierarchyId,
            memberUsernames
        );

        projectResult.Value.AddProjectHierarchy(hierarchy);

        _unitOfWork.AddEventsQueue(projectResult.Value.DomainEvents);

        return await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

