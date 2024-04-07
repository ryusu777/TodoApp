using Library.Models;

namespace ProjectManagement.Domain.Project.ValueObjects;

public sealed class SubdomainKnowledgeId : ValueObject
{
    public Guid Value { get; init; }
    private SubdomainKnowledgeId(Guid value) 
    {
        Value = value;
    }
    public static SubdomainKnowledgeId Create(Guid value) => new(value);
    public static SubdomainKnowledgeId CreateUnique() => new(Guid.NewGuid());
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
