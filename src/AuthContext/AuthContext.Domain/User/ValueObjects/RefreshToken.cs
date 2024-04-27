using Library.Models;

namespace AuthContext.Domain.User.ValueObjects;

public class RefreshToken : ValueObject
{
    public string Value { get; init; }

    private RefreshToken(string value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static RefreshToken Create(string value) 
    {
        return new(value);
    }
}
