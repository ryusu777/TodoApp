using MediatR;
using IntegrationContext.Infrastructure.Persistence.Data;

namespace IntegrationContext.Infrastructure.Persistence;

public interface IPersistEventHandler<T>
	where T : INotification
{
	public Task Handle(T notification, AppDbContext dbContext, CancellationToken cancellationToken);
}
