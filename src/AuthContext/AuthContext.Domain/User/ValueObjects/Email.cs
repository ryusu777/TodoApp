using Library.Models;

namespace AuthContext.Domain.User.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; init; }

    private Email(string value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static Email Create(string value) 
    {
        return new(value);
    }
}
