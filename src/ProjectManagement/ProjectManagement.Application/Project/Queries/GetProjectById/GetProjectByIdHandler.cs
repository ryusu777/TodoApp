using Library.Models;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Project.Dtos;
using ProjectManagement.Domain.Project;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Queries.GetProjectById;

public class GetProjectByIdHandler : IQueryHandler<GetProjectByIdQuery, GetProjectByIdResult>
{
    private readonly IProjectRepository _repository;

    public GetProjectByIdHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetProjectByIdResult>> Handle(
        GetProjectByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var result = await _repository
            .GetProjectById(ProjectId.Create(request.Id), cancellationToken);

        var project = result.Value;

        if (project is null)
        {
            return Result.Failure<GetProjectByIdResult>(ProjectDomainErrors.ProjectNotFound);
        }
        
        return Result.Success(new GetProjectByIdResult(
            project.Id.Value,
            project.Name,
            project.Description,
            (int)project.Status,
            project.ProjectPhases.Select(e => Phase.FromDomain(e)!).ToList(),
            project.ProjectMembers.Select(e => e.Value).ToList()
        ));
    }
}

