using Library.Models;

namespace ProjectManagement.Domain.Project.ValueObjects;

public sealed class PhaseName : ValueObject
{
    public Guid Value { get; init; }
    private PhaseName(Guid value) 
    {
        Value = value;
    }
    public static PhaseName CreateUnique() 
    {
        return new(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
