using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Assignment;
using ProjectManagement.Application.Project.Events;
using ProjectManagement.Domain.Project;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Commands.DeleteProject;

public sealed class DeleteProjectCommandHandler : ICommandHandler<DeleteProjectCommand>
{
	private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectRepository _projectRepository;
    private readonly IAssignmentRepository _assignmentRepository;
    public DeleteProjectCommandHandler(IUnitOfWork unitOfWork, IProjectRepository projectRepository, IAssignmentRepository assignmentRepository)
    {
        _unitOfWork = unitOfWork;
        _projectRepository = projectRepository;
        _assignmentRepository = assignmentRepository;

    }
    public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
	{
        var result = await _projectRepository.GetProjectById(ProjectId.Create(request.ProjectId), cancellationToken);

        if (result.Value is null)
        {
            return result;
        }

        var assignments = await _assignmentRepository
            .GetAssignments(ProjectId.Create(request.ProjectId), cancellationToken);

        if (assignments.Value!.Any())
        {
            return ProjectDomainErrors.CannotDeleteProjectWithAssignment;
        }

        return await _unitOfWork
            .SaveChangesAsync(new ProjectDeleted(result.Value), cancellationToken);
	}
}
