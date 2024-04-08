using Library.Models;

namespace ProjectManagement.Domain.Project.ValueObjects;

public sealed class PhaseId : ValueObject
{
    public Guid Value { get; init; }
    private PhaseId(Guid value) 
    {
        Value = value;
    }
    public static PhaseId Create(Guid value)
    {
        return new(value);
    }
    public static PhaseId CreateUnique() 
    {
        return new(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
