namespace MassTransitContracts.Data.Entities;

public class OutboxMessage
{
    public required Guid Id { get; set; }
    public required string EventDetail { get; set; }
    public string? ErrorMessage { get; set; }
    public int Tries { get; set; } = 0;
    public int MaxTries { get; set; } = 5;
    public DateTime? PublishedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastTryAt { get; set; } = DateTime.Now;
}
