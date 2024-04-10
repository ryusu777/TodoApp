using Library.Models;

namespace ProjectManagement.Application.Abstractions.Data;

public interface IPersistorQueueMaker
{
	public void AddEventsQueue(IReadOnlyCollection<IDomainEvent> domainEvents);
	public void AddEventQueue(IDomainEvent domainEvents);
}
