using Library.Models;

namespace IntegrationContext.Domain.GiteaRepositories.ValueObjects;

public class RepositoryHookId : ValueObject
{
    public int Value { get; init; }

    private RepositoryHookId(int value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static RepositoryHookId Create(int value) 
    {
        return new(value);
    }
}
