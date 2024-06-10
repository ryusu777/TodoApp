using Library.Models;

namespace ProjectManagement.Domain.Assignment.ValueObjects;

public class ReviewId : ValueObject
{
    public Guid Value { get; init; }
    private ReviewId(Guid value) 
    {
        Value = value;
    }
    public static ReviewId Create(Guid value)
    {
        return new(value);
    }
    public static ReviewId CreateUnique() 
    {
        return new(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
