using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Project;
using ProjectManagement.Domain.Assignment.Events;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Assignment.Commands.CreateAssignmentFromHook;

public class CreateAssignmentFromHookCommandHandler : ICommandHandler<CreateAssignmentFromHookCommand>
{
	public readonly IProjectRepository _projectRepository;
	public readonly IUnitOfWork _unitOfWork;

	public CreateAssignmentFromHookCommandHandler(
		IUnitOfWork unitOfWork, 
		IProjectRepository projectRepository)
	{
		_unitOfWork = unitOfWork;
		_projectRepository = projectRepository;
	}

	public async Task<Result> Handle(CreateAssignmentFromHookCommand request, CancellationToken cancellationToken)
	{
		var projectResult = await _projectRepository
			.GetProjectById(ProjectId.Create(request.Message.ProjectId), cancellationToken);

		if (projectResult.Value is null)
		{
			return projectResult;
		}

		var createdAssignment = Domain.Assignment.Assignment.Create(
            AssignmentId.Create(request.Message.AssignmentId),
			request.Message.Title,
			request.Message.Body,
			ProjectId.Create(request.Message.ProjectId),
            request.Message.Assignees
                .Select(e => UserId.Create(e))
                .ToList(),
            null,
            request.Message.DueDate
        );

		return await _unitOfWork
            .SaveChangesAsync(new AssignmentCreatedFromHook(
                createdAssignment), cancellationToken);
	}
}
