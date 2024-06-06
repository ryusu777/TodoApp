using Library.Models;

namespace ProjectManagement.Domain.Project.ValueObjects;

public sealed class HierarchyId : ValueObject
{
    public Guid Value { get; init; }

    private HierarchyId(Guid value) 
    {
        Value = value;
    }
    public static HierarchyId Create(Guid value)
    {
        return new(value);
    }
    public static HierarchyId CreateUnique() 
    {
        return new(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
