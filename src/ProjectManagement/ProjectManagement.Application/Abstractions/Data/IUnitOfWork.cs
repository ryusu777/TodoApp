using Library.Models;

namespace ProjectManagement.Application.Abstractions.Data;

public interface IUnitOfWork : IPersistorQueueMaker
{
	public Task<Result> SaveChangesAsync(CancellationToken cancellationToken);
	public Task<Result> SaveChangesAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken);
	public Task<Result> SaveChangesAsync(IDomainEvent domainEvent, CancellationToken cancellationToken);
}
