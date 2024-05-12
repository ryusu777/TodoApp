using Library.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
    public DateTime GetExpiryInUtc()
    {
        var handler = new JwtSecurityTokenHandler();

        JwtSecurityToken readToken = handler.ReadJwtToken(Value);

        Claim? expClaim = readToken
            .Claims
            .FirstOrDefault(e => e.Type == JwtRegisteredClaimNames.Exp);

        return readToken.ValidTo;
    }

    public bool IsExpired()
    {
        return GetExpiryInUtc() <= DateTime.UtcNow;
    }
}
