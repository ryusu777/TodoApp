using IntegrationContext.Domain.Auth;
using IntegrationContext.Domain.Auth.Events;
using IntegrationContext.Infrastructure.Persistence;
using IntegrationContext.Infrastructure.Persistence.Data;

namespace IntegrationContext.Infrastructure.Auth;

public class AuthPersistorHandler : IPersistEventHandler<GiteaUserCreated>
{
	public Task Handle(GiteaUserCreated notification, AppDbContext dbContext, CancellationToken cancellationToken)
	{
		dbContext.GiteaUsers.Add(notification.User);

		return Task.CompletedTask;
	}
}
