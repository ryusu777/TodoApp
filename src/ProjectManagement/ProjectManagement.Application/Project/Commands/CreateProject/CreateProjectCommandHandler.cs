using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Project.Events;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Commands.CreateProject;

public sealed class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateProjectCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
	{
        var created = Domain.Project.Project.Create(
            request.Code,
            request.Name,
            request.Description,
            request.ProjectMembers.Select(e => UserId.Create(e)).ToList(),
            request.ProjectPhases
                .Select(e => e.ToDomain()).ToList());

        return await _unitOfWork.SaveChangesAsync(new ProjectCreated(created), cancellationToken);
	}
}
