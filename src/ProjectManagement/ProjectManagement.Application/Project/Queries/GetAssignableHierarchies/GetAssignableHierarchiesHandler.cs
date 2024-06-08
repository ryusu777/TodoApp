using Library.Models;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Queries.GetAssignableHierarchies;

public class GetAssignableHierarchiesHandler : IQueryHandler<GetAssignableHierarchiesQuery, GetAssignableHierarchiesResult>
{
    private readonly IProjectRepository _projectRepository;

    public GetAssignableHierarchiesHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<GetAssignableHierarchiesResult>> Handle(GetAssignableHierarchiesQuery request, CancellationToken cancellationToken)
    {
        var projectResult = await _projectRepository.GetProjectById(ProjectId.Create(request.ProjectId), cancellationToken);

        if (projectResult.IsFailure || projectResult.Value is null)
        {
            return Result.Failure<GetAssignableHierarchiesResult>(projectResult.Error);
        }

        var result = projectResult.Value
            .GetAssignableHierarchies(UserId.Create(request.UserId));

        if (result.IsFailure || result.Value is null)
        {
            return Result.Failure<GetAssignableHierarchiesResult>(result.Error);
        }

        return Result.Success(new GetAssignableHierarchiesResult(
            request.ProjectId, 
            result.Value
                .Select(e => Dtos.Hierarchy.FromDomain(e)!)
                .ToList()));
    }
}
