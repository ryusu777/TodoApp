using Library.Models;

namespace IntegrationContext.Domain.GiteaIssues.ValueObjects;

public class ProjectId : ValueObject
{
    public string Value { get; init; }

    private ProjectId(string value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static ProjectId Create(string value) 
    {
        return new(value);
    }
}
