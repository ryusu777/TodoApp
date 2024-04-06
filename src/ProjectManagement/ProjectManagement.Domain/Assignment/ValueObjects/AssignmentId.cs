using Library.Models;

namespace ProjectManagement.Domain.Assignment.ValueObjects;

public sealed class AssignmentId : ValueObject
{
    public Guid Value { get; init; }
    private AssignmentId(Guid value) 
    {
        Value = value;
    }
    public static AssignmentId Create(Guid value)
    {
        return new(value);
    }
    public static AssignmentId CreateUnique() 
    {
        return new(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
