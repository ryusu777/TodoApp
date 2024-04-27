using Library.Models;

namespace AuthContext.Domain.User.ValueObjects;

public class Jti : ValueObject
{
    public string Value { get; init; }

    private Jti(string value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static Jti Create(string value) 
    {
        return new(value);
    }
}
