using Library.Models;

namespace IntegrationContext.Domain.CommandOutboxes.ValueObjects;

public class CommandOutboxId : ValueObject
{
    public Guid Value { get; init; }

    private CommandOutboxId(Guid value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static CommandOutboxId Create(Guid value) 
    {
        return new(value);
    }
    public static CommandOutboxId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
}

