using Library.Models;

namespace IntegrationContext.Domain.GiteaIssues.ValueObjects;

public class IssueNumber : ValueObject
{
    public int Value { get; init; }

    private IssueNumber(int value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static IssueNumber Create(int value) 
    {
        return new(value);
    }
}
