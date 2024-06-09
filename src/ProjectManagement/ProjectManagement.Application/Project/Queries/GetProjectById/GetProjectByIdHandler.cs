using Library.Models;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Assignment;
using ProjectManagement.Application.Project.Dtos;
using ProjectManagement.Domain.Project;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Queries.GetProjectById;

public class GetProjectByIdHandler : IQueryHandler<GetProjectByIdQuery, GetProjectByIdResult>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IAssignmentRepository _assignmentRepository;

    public GetProjectByIdHandler(IProjectRepository repository, IAssignmentRepository assignmentRepository)
    {
        _projectRepository = repository;
        _assignmentRepository = assignmentRepository;
    }

    public async Task<Result<GetProjectByIdResult>> Handle(
        GetProjectByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var result = await _projectRepository
            .GetProjectById(ProjectId.Create(request.Id), cancellationToken);

        var project = result.Value;

        if (project is null)
        {
            return Result.Failure<GetProjectByIdResult>(ProjectDomainErrors.ProjectNotFound);
        }

        var newAssignmentCountResult = await _assignmentRepository
            .GetNewAssignmentCount(ProjectId.Create(request.Id), cancellationToken);
        
        return Result.Success(new GetProjectByIdResult(
            project.Id.Value,
            project.Name,
            project.Description,
            (int)project.Status,
            project.ProjectPhases.Select(e => Phase.FromDomain(e)!).ToList(),
            project.Hierarchies.Select(e => Hierarchy.FromDomain(e)!).ToList(),
            project.Members.Select(e => e.Value).ToList(),
            newAssignmentCountResult.Value
        ));
    }
}

