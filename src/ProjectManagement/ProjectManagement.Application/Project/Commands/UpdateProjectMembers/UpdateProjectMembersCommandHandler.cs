using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Commands.UpdateProjectMembers;

public class UpdateProjectMembersCommandHandler : ICommandHandler<UpdateProjectMembersCommand>
{
	private readonly IProjectRepository _projectRepository;
	private readonly IUnitOfWork _unitOfWork;
    public UpdateProjectMembersCommandHandler(
		IProjectRepository projectRepository,
		IUnitOfWork unitOfWork
	) {
        _projectRepository = projectRepository;
		_unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(UpdateProjectMembersCommand request, CancellationToken cancellationToken)
	{
		var result = await _projectRepository.GetProjectById(ProjectId.Create(request.ProjectId));

        if (result.Value is null)
        {
            return result;
        }

        result.Value.UpdateProjectMembers(request
            .MemberUsernames.Select(e => UserId.Create(e)).ToList());

        return await _unitOfWork.SaveChangesAsync(result.Value.DomainEvents, cancellationToken);
	}
}
