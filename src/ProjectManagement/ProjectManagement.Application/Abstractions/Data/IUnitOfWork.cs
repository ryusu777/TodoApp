using Library.Models;

namespace ProjectManagement.Application.Abstractions.Data;

public interface IUnitOfWork
{
	public Task<Result> SaveChangesAsync(CancellationToken cancellationToken);
	public Task<Result> SaveChangesAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken);
	public void AddEventsQueue(IReadOnlyCollection<IDomainEvent> domainEvents);
}
