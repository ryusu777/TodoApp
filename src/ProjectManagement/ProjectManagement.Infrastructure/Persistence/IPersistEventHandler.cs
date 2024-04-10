using MediatR;
using ProjectManagement.Infrastructure.Persistence.Data;

namespace ProjectManagement.Infrastructure.Persistence;

public interface IPersistEventHandler<T>
	where T : INotification
{
	public Task Handle(T notification, AppDbContext dbContext, CancellationToken cancellationToken);
}
