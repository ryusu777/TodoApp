using Library.Models;

namespace IntegrationContext.Domain.GiteaIssues.ValueObjects;

public class GiteaIssueId : ValueObject
{
    public int Value { get; init; }

    private GiteaIssueId(int value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static GiteaIssueId Create(int value) 
    {
        return new(value);
    }
}
