namespace ProjectManagement.Application.Assignment.Queries.GetAssignments;

public class GetAssignmentsResult : List<Dtos.Assignment>
{
    public GetAssignmentsResult(IEnumerable<Dtos.Assignment> collection) : base(collection)
    {
    }
}
