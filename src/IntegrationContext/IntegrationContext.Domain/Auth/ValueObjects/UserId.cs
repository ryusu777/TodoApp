using Library.Models;

namespace IntegrationContext.Domain.Auth.ValueObjects;

public class UserId : ValueObject
{
    public string Value { get; init; }

    private UserId(string value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static UserId Create(string username) 
    {
        return new(username);
    }
}
