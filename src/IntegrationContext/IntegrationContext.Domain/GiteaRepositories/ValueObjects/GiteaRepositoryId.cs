using Library.Models;

namespace IntegrationContext.Domain.GiteaRepositories.ValueObjects;

public class GiteaRepositoryId : ValueObject
{
    public int Value { get; init; }

    private GiteaRepositoryId(int value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static GiteaRepositoryId Create(int value) 
    {
        return new(value);
    }
}
