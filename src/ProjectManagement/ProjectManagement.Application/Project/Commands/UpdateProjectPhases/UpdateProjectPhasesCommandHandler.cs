using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Commands.UpdateProjectPhases;

public class UpdateProjectPhasesCommandHandler : ICommandHandler<UpdateProjectPhasesCommand>
{
	private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateProjectPhasesCommandHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork
    ) {
		_projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(UpdateProjectPhasesCommand request, CancellationToken cancellationToken)
	{
		var result = await _projectRepository.GetProjectById(ProjectId.Create(request.ProjectId));

        if (result.Value is null)
        {
            return result;
        }

        result.Value.UpdateProjectPhases(request.Phases);

        return await _unitOfWork.SaveChangesAsync(result.Value.DomainEvents, cancellationToken);
    }
}
