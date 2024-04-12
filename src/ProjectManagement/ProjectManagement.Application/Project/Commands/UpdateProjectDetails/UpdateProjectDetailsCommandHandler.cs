using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Commands.UpdateProjectDetails;

public sealed class UpdateProjectDetailsCommandHandler : ICommandHandler<UpdateProjectDetailsCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IProjectRepository _projectRepo;

    public UpdateProjectDetailsCommandHandler(IUnitOfWork unitOfWork, IProjectRepository repo)
    {
        _unitOfWork = unitOfWork;
        _projectRepo = repo;
    }
    public async Task<Result> Handle(UpdateProjectDetailsCommand request, CancellationToken cancellationToken)
	{
		var result = await _projectRepo.GetProjectById(ProjectId.Create(request.ProjectId), cancellationToken);

        if (result.Value is null)
        {
            return result;
        }

        result.Value.UpdateProject(request.Name, request.Description, request.Status);

        return await _unitOfWork.SaveChangesAsync(result.Value.DomainEvents, cancellationToken);
    }
}
