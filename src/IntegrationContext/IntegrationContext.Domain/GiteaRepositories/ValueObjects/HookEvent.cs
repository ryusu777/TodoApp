using Library.Models;

namespace IntegrationContext.Domain.GiteaRepositories.ValueObjects;

public class HookEvent : ValueObject
{
    public string Value { get; init; }

    private HookEvent(string value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static HookEvent Create(string value) 
    {
        return new(value);
    }
}
