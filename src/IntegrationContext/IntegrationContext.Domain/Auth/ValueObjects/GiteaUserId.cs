using Library.Models;

namespace IntegrationContext.Domain.Auth.ValueObjects;

public class GiteaUserId : ValueObject
{
    public int Value { get; init; }

    private GiteaUserId(int value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static GiteaUserId Create(int value) 
    {
        return new(value);
    }
}
