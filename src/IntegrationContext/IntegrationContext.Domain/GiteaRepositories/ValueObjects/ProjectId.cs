using Library.Models;

namespace IntegrationContext.Domain.GiteaIssues.ValueObjects;

public class ProjectId : ValueObject
{
    public Guid Value { get; init; }

    private ProjectId(Guid value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static ProjectId Create(Guid value) 
    {
        return new(value);
    }
}
