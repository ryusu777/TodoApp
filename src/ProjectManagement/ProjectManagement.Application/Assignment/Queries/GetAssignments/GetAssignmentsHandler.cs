using Library.Models;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Assignment.Queries.GetAssignments;

public class GetAssignmentsHandler
    : IQueryHandler<GetAssignmentsQuery, GetAssignmentsResult>
{
    private readonly IAssignmentRepository _repo;

    public GetAssignmentsHandler(IAssignmentRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<GetAssignmentsResult>> Handle(GetAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var result = await _repo
            .GetAssignmentsBySubdomain(
                ProjectId.Create(request.ProjectId), 
                request.SubdomainId is not null 
                    ? SubdomainId.Create((Guid)request.SubdomainId)
                    : null,
                cancellationToken);

        if (result.IsFailure || result.Value is null)
            return Result.Failure<GetAssignmentsResult>(result.Error);

        return Result.Success(new GetAssignmentsResult(result.Value));
    }
}

