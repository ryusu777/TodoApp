using ProjectManagement.Application.Project.Events;
using ProjectManagement.Domain.Project.Events;
using ProjectManagement.Infrastructure.Persistence;
using ProjectManagement.Infrastructure.Persistence.Data;

namespace ProjectManagement.Infrastructure.Project;

public class ProjectPersistHandler :
	IPersistEventHandler<ProjectCreated>,
	IPersistEventHandler<ProjectDeleted>,
    IPersistEventHandler<ProjectDetailsUpdated>
{
	public Task Handle(ProjectCreated notification, AppDbContext dbContext, CancellationToken cancellationToken)
	{
		dbContext.Projects.Add(notification.Project);
		return Task.CompletedTask;
	}

	public Task Handle(ProjectDeleted notification, AppDbContext dbContext, CancellationToken cancellationToken)
	{
		dbContext.Projects.Remove(notification.Project);
		return Task.CompletedTask;
	}

    public Task Handle(ProjectDetailsUpdated notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        dbContext.Projects.Update(notification.Project);
        return Task.CompletedTask;
    }
}
