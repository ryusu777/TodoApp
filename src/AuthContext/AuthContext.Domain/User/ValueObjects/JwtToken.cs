using Library.Models;

namespace AuthContext.Domain.User.ValueObjects;

public class JwtToken : ValueObject
{
    public string Value { get; init; }

    private JwtToken(string value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static JwtToken Create(string value) 
    {
        return new(value);
    }
}
