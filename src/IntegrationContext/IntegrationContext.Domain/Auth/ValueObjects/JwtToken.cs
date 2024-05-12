using Library.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IntegrationContext.Domain.Auth.ValueObjects;

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
