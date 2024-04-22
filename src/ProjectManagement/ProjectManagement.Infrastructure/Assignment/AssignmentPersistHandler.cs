namespace ProjectManagement.Infrastructure.Assignment;

using ProjectManagement.Application.Assignment.Events;
using ProjectManagement.Domain.Assignment.Events;
using ProjectManagement.Infrastructure.Persistence;
using ProjectManagement.Infrastructure.Persistence.Data;

public class AssignmentPersistHandler :
    IPersistEventHandler<AssignmentCreated>,
    IPersistEventHandler<AssignmentDeleted>,
    IPersistEventHandler<AssigneeRemoved>,
    IPersistEventHandler<AssignmentAssigned>,
    IPersistEventHandler<AssignmentCompleted>,
    IPersistEventHandler<AssignmentRenewed>,
    IPersistEventHandler<AssignmentReviewRequested>,
    IPersistEventHandler<AssignmentUpdated>,
    IPersistEventHandler<AssignmentWorkedOn>
{
    private readonly AppDbContext _dbContext;

    public AssignmentPersistHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(AssignmentWorkedOn notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(AssignmentReviewRequested notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(AssignmentRenewed notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(AssignmentCompleted notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(AssignmentAssigned notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(AssigneeRemoved notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
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

    public Task Handle(AssignmentUpdated notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

