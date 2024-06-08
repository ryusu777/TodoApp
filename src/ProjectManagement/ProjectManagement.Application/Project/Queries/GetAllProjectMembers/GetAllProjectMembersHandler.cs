using Library.Models;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Queries.GetAllProjectMembers;

public class GetAllProjectMembersHandler : IQueryHandler<GetAllProjectMembersQuery, GetAllProjectMembersResult>
{
    private readonly IProjectRepository _projectRepository;

    public GetAllProjectMembersHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<GetAllProjectMembersResult>> Handle(GetAllProjectMembersQuery request, CancellationToken cancellationToken)
    {
        var result = await _projectRepository.GetProjectById(ProjectId.Create(request.ProjectId), cancellationToken);

        if (result.IsFailure || result.Value is null)
        {
            return Result.Failure<GetAllProjectMembersResult>(result.Error);
        }

        return Result.Success(new GetAllProjectMembersResult(
            request.ProjectId, 
            result.Value.GetAllProjectMembers().Select(x => x.Value).ToList()));
    }
}

