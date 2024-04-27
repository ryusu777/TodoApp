using MediatR;
using AuthContext.Infrastructure.Persistence.Data;

namespace AuthContext.Infrastructure.Persistence;

public interface IPersistEventHandler<T>
	where T : INotification
{
	public Task Handle(T notification, AppDbContext dbContext, CancellationToken cancellationToken);
}
