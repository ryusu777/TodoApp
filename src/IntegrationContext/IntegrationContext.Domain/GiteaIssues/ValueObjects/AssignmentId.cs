using Library.Models;

namespace IntegrationContext.Domain.GiteaIssues.ValueObjects;

public class AssignmentId : ValueObject
{
    public Guid Value { get; init; }

    private AssignmentId(Guid value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static AssignmentId Create(Guid value) 
    {
        return new(value);
    }
}
