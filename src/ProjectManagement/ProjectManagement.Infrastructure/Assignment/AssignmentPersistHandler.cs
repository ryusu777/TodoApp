namespace ProjectManagement.Infrastructure.Assignment;

using ProjectManagement.Domain.Assignment.Events;
using ProjectManagement.Infrastructure.Persistence;
using ProjectManagement.Infrastructure.Persistence.Data;

public class AssignmentPersistHandler :
    IPersistEventHandler<AssignmentCreated>,
    IPersistEventHandler<AssignmentDeleted>,
    IPersistEventHandler<AssignmentCreatedFromHook>
{
    private readonly AppDbContext _dbContext;

    public AssignmentPersistHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(AssignmentDeleted notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        dbContext
            .Assignments
            .Remove(notification.Assignment);
        return Task.CompletedTask;
    }

    public Task Handle(AssignmentCreated notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        dbContext.Assignments.Add(notification.Assignment);
        return Task.CompletedTask;
    }

    public Task Handle(AssignmentCreatedFromHook notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        dbContext.Assignments.Add(notification.Assignment);
        return Task.CompletedTask;
    }
}

