using IntegrationContext.Application.Abstractions.Messaging;
using IntegrationContext.Domain.GiteaIssues.ValueObjects;
using Library.Models;

namespace IntegrationContext.Application.GiteaIssues.Queries.GetAssignmentsIssueNumber;

public class GetAssignmentsIssueNumberHandler : IQueryHandler<GetAssignmentsIssueNumberQuery, GetAssignmentsIssueNumberResult>
{
    private readonly IGiteaIssueRepository _issueRepository;

    public GetAssignmentsIssueNumberHandler(IGiteaIssueRepository issueRepository)
    {
        _issueRepository = issueRepository;
    }

    public async Task<Result<GetAssignmentsIssueNumberResult>> Handle(GetAssignmentsIssueNumberQuery request, CancellationToken cancellationToken)
    {
        var result = await _issueRepository
            .GetAssignmentsIssueNumberAsync(
                request
                    .AssignmentIds
                    .Select(e => AssignmentId.Create(e))
                    .ToList(), 
                cancellationToken);

        if (result.IsFailure || result.Value is null)
            return Result.Failure<GetAssignmentsIssueNumberResult>(result.Error);

        return Result.Success(new GetAssignmentsIssueNumberResult(result.Value));
    }
}

