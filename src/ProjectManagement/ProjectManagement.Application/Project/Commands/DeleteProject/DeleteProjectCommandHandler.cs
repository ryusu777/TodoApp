using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Project.Events;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Commands.DeleteProject;

public sealed class DeleteProjectCommandHandler : ICommandHandler<DeleteProjectCommand>
{
	private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectRepository _projectRepository;
    public DeleteProjectCommandHandler(IUnitOfWork unitOfWork, IProjectRepository projectRepository)
    {
        _unitOfWork = unitOfWork;
        _projectRepository = projectRepository;
    }
    public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
	{
        var result = await _projectRepository.GetProjectById(ProjectId.Create(request.ProjectId));

        if (result.Value is null)
        {
            return result;
        }

        return await _unitOfWork
            .SaveChangesAsync(new ProjectDeleted(result.Value), cancellationToken);
	}
}
