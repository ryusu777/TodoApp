using Library.Models;

namespace AuthContext.Application.Abstractions.Data;

public interface IPersistorQueueMaker
{
	public void AddEventsQueue(IReadOnlyCollection<IDomainEvent> domainEvents);
	public void AddEventQueue(IDomainEvent domainEvents);
}
