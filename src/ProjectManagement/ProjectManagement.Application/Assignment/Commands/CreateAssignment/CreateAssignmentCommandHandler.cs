using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Project;
using ProjectManagement.Domain.Assignment.Events;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Assignment.Commands.CreateAssignment;

public class CreateAssignmentCommandHandler : ICommandHandler<CreateAssignmentCommand>
{
	public readonly IProjectRepository _projectRepository;
	public readonly IUnitOfWork _unitOfWork;

	public CreateAssignmentCommandHandler(
		IUnitOfWork unitOfWork, 
		IProjectRepository projectRepository)
	{
		_unitOfWork = unitOfWork;
		_projectRepository = projectRepository;
	}

	public async Task<Result> Handle(CreateAssignmentCommand request, CancellationToken cancellationToken)
	{
		var projectResult = await _projectRepository
			.GetProjectById(ProjectId.Create(request.ProjectId), cancellationToken);

		if (projectResult.Value is null)
		{
			return projectResult;
		}

		var createdAssignment = Domain.Assignment.Assignment.Create(
			request.Title,
			request.Description,
			ProjectId.Create(request.ProjectId),
            request.Assignees
                .Select(e => UserId.Create(e))
                .ToList(),
            request.SubdomainId.HasValue ? SubdomainId.Create(request.SubdomainId.Value) : null,
            request.Deadline,
            request.PhaseId.HasValue ? PhaseId.Create(request.PhaseId.Value) : null,
            request.Reviewer is not null ? UserId.Create(request.Reviewer) : null);

		return await _unitOfWork
            .SaveChangesAsync(new AssignmentCreated(
                createdAssignment, 
                UserId.Create(request.UserId),
                request.GiteaRepositoryId), cancellationToken);
	}
}
