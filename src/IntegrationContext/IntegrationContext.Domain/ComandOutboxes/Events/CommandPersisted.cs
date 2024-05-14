using Library.Models;

namespace IntegrationContext.Domain.CommandOutboxes.Events;

public record CommandPersisted(CommandOutbox Command) : IDomainEvent;
