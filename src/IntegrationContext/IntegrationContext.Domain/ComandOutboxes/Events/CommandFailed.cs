using Library.Models;

namespace IntegrationContext.Domain.CommandOutboxes.Events;

public record CommandFailed(CommandOutbox Command) : IDomainEvent;
