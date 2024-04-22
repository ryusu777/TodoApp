using Library.Models;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Assignment.ValueObjects;

namespace ProjectManagement.Application.Assignment.Queries.GetAssignment;

public class GetAssignmentHandler
    : IQueryHandler<GetAssignmentQuery, Dtos.Assignment>
{
    private readonly IAssignmentRepository _repo;

    public GetAssignmentHandler(IAssignmentRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<Dtos.Assignment>> Handle(GetAssignmentQuery request, CancellationToken cancellationToken)
    {
        var result = await _repo
            .GetAssignmentAsDtoById(AssignmentId.Create(request.AssignmentId), cancellationToken);

        if (result.IsFailure || result.Value is null)
            return Result.Failure<Dtos.Assignment>(result.Error);

        var assignment = result.Value;

        return Result.Success(result.Value);
    }
}

