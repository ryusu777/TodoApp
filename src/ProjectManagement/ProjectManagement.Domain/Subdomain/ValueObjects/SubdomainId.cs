using Library.Models;

namespace ProjectManagement.Domain.Project.ValueObjects;

public sealed class SubdomainId : ValueObject
{
    public Guid Value { get; init; }
    private SubdomainId(Guid value) 
    {
        Value = value;
    }
    public static SubdomainId CreateUnique() 
    {
        return new(Guid.NewGuid());
    }
    public static SubdomainId Create(Guid Value) 
    {
        return new(Value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
