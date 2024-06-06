using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Project;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Commands.UpdateProjectHierarchyDetails;
public class UpdateProjectHierarchyDetailsCommandHandler : ICommandHandler<UpdateProjectHierarchyDetailsCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectHierarchyDetailsCommandHandler(
        IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateProjectHierarchyDetailsCommand request, CancellationToken cancellationToken)
    {
        var projectResult = await _projectRepository.GetProjectById(ProjectId.Create(request.ProjectId), cancellationToken);

        if (projectResult.IsFailure || projectResult.Value is null)
        {
            return projectResult;
        }

        var hierarchy = projectResult.Value.Hierarchies.FirstOrDefault(h => h.Id == HierarchyId.Create(request.HierarchyId));

        if (hierarchy is null)
        {
            return ProjectDomainErrors.HierarchyNotFound(HierarchyId.Create(request.HierarchyId));
        }

        projectResult.Value
            .UpdateProjectHierarchyDetail(
                HierarchyId.Create(request.HierarchyId), 
                request.Name, 
                !request.SuperiorHierarchyId.HasValue
                    ? null 
                    : HierarchyId.Create(request.SuperiorHierarchyId.Value));

        return await _unitOfWork.SaveChangesAsync(projectResult.Value.DomainEvents, cancellationToken);
    }
}
