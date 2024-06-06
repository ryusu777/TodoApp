using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Commands.UpdateProjectHierarchyMembers;

public class UpdateProjectHierarchyMembersCommandHandler : ICommandHandler<UpdateProjectHierarchyMembersCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectHierarchyMembersCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateProjectHierarchyMembersCommand request, CancellationToken cancellationToken)
    {
        var projectResult = await _projectRepository.GetProjectById(ProjectId.Create(request.ProjectId), cancellationToken);

        if (projectResult.IsFailure || projectResult.Value is null)
        {
            return projectResult;
        }

        projectResult.Value
            .UpdateProjectHierarchyMembers(
                HierarchyId.Create(request.HierarchyId), 
                request.MemberUsernames
                    .Select(x => UserId.Create(x))
                    .ToList());

        return await _unitOfWork.SaveChangesAsync(projectResult.Value.DomainEvents, cancellationToken);
    }
}
