using Library.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Infrastructure.Persistence.Data;
using IntegrationContext.Infrastructure.Persistence.Mediator;

namespace IntegrationContext.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private List<IDomainEvent> _populatedDomainEvents = new List<IDomainEvent>();
    private readonly AppDbContext _dbContext;
    private readonly PersistPublisher _persistPublisher;
    private readonly IPublisher _publisher;

	public UnitOfWork(AppDbContext dbContext, PersistPublisher persistPublisher, IPublisher publisher)
	{
		_dbContext = dbContext;
		_persistPublisher = persistPublisher;
		_publisher = publisher;
	}

	public void AddEventQueue(IDomainEvent domainEvent)
    {
        _populatedDomainEvents.Add(domainEvent);
    }

    public void AddEventsQueue(IReadOnlyCollection<IDomainEvent> domainEvents)
    {
        _populatedDomainEvents.AddRange(domainEvents);
    }

    public async Task<Result> SaveChangesAsync(CancellationToken cancellationToken)
    {
        foreach (var domainEvent in _populatedDomainEvents)
        {
            Task task = (Task)_persistPublisher
                .GetType()
                .GetMethod(nameof(PersistPublisher.Publish))!
                .MakeGenericMethod(domainEvent.GetType())
                .Invoke(_persistPublisher, [domainEvent, _dbContext, cancellationToken])!;

            await task.ConfigureAwait(false);
        }

        try
        {
            await _dbContext.SaveChangesAsync();

			foreach (var domainEvent in _populatedDomainEvents)
			{
				await _publisher.Publish(domainEvent, cancellationToken);
			}

            _populatedDomainEvents.RemoveAll(e => true);

            return Result.Success();
        }
        catch (DbUpdateException e)
        {
            return Result
                .Failure(new Error("DbUpdateException", e.InnerException?.Message ?? e.Message));
        }
    }

    public async Task<Result> SaveChangesAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        AddEventsQueue(domainEvents);
        return await SaveChangesAsync(cancellationToken);
    }

    public async Task<Result> SaveChangesAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        AddEventQueue(domainEvent);
        return await SaveChangesAsync(cancellationToken);
    }
}
