using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Commands.DeleteProjectHierarchy;

public class DeleteProjectHierarchyCommandHandler : ICommandHandler<DeleteProjectHierarchyCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProjectHierarchyCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteProjectHierarchyCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetProjectById(ProjectId.Create(request.ProjectId), cancellationToken);

        if (project.IsFailure || project.Value is null)
        {
            return project;
        }
        
        var result = project.Value.DeleteProjectHierarchy(HierarchyId.Create(request.HierarchyId));

        if (result.IsFailure)
            return result;

        _unitOfWork.AddEventsQueue(project.Value.DomainEvents);

        return await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

