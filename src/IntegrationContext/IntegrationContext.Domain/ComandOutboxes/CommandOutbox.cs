using IntegrationContext.Domain.CommandOutboxes.ValueObjects;
using Library.Models;

namespace IntegrationContext.Domain.CommandOutboxes;

public class CommandOutbox : AggregateRoot<CommandOutboxId>
{
#pragma warning disable CS8618
    private CommandOutbox() { }
#pragma warning restore CS8618

    public string CommandDetail { get; private set; }
    public string? CommandResult { get; private set; }
    public int Tries { get; private set; } = 0;
    public int MaxTries { get; private set; }
    public DateTime? SuccessAt { get; private set; }

    public bool IsSuccess => SuccessAt is not null;
    public bool IsMaxTries => Tries >= MaxTries;

    private CommandOutbox(
        CommandOutboxId id,
        string commandDetail,
        int maxTries = 5) : base(id)
    {
        CommandDetail = commandDetail;
        MaxTries = maxTries;
    }

    public static CommandOutbox Create(
        string commandDetail,
        int maxTries = 5)
    {
        return new(CommandOutboxId.CreateUnique(), commandDetail, maxTries);
    }

    public void Success(string commandResult)
    {
        CommandResult = commandResult;
        SuccessAt = DateTime.Now;
    }

    public void IncrementTries()
    {
        Tries++;
    }
}
