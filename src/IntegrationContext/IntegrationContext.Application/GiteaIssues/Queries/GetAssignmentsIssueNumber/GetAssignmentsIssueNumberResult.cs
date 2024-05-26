using IntegrationContext.Application.GiteaIssues.Dtos;

namespace IntegrationContext.Application.GiteaIssues.Queries.GetAssignmentsIssueNumber;

public class GetAssignmentsIssueNumberResult : List<AssignmentIssueNumber>
{
    public GetAssignmentsIssueNumberResult(IEnumerable<AssignmentIssueNumber> collection) : base(collection)
    {
    }
}
