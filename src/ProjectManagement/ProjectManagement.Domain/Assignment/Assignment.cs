using Library.Models;
using ProjectManagement.Domain.Assignment.Enums;
using ProjectManagement.Domain.Assignment.Events;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Assignment;

public sealed class Assignment : AggregateRoot<AssignmentId>
{
    private Assignment(
        AssignmentId id, 
        string title, 
        string description, 
        ProjectId projectId) : base(id)
    {
        Title = title;
        Description = description;
        ProjectId = projectId;
        Status = new AssignmentStatus(AssignmentStatusEnum.New);
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public ProjectId ProjectId { get; set; }
    public AssignmentStatus Status { get; set; }
    public ICollection<SubdomainId> SubdomainIds { get; set; } = new List<SubdomainId>();
    public ICollection<UserId> Assignees { get; set; } = new List<UserId>();

    public static Assignment Create(
        string title, 
        string description, 
        ProjectId projectId)
    {
        var result = new Assignment(
            AssignmentId.CreateUnique(),
            title,
            description,
            projectId
		);
        return result;
    }

    public Result Assign(UserId userId)
    {
        if (Assignees.Any(e => e == userId))
            return AssignmentDomainErrors.AssigneeAlreadyExists;

        Assignees.Add(userId);

        RaiseDomainEvent(new AssignmentAssigned(Id, userId));

        return Result.Success();
    }

    public Result RemoveAssignee(UserId userId)
    {
        UserId? assignee = Assignees.FirstOrDefault(e => e == userId);
        if (assignee is null)
            return AssignmentDomainErrors.AssigneeNotFound;

        Assignees.Remove(assignee);

        RaiseDomainEvent(new AssigneeRemoved(Id, userId));

        return Result.Success();
    }

    public Result WorkOn()
    {
        if (Status.Value != AssignmentStatusEnum.New || Status.Value != AssignmentStatusEnum.WaitingReview)
            return AssignmentDomainErrors.AssignmentIsNotAvailableToWorkOn;

        Status = new AssignmentStatus(AssignmentStatusEnum.OnProgress);
        RaiseDomainEvent(new AssignmentWorkedOn(Id));

        return Result.Success();
    }

    public Result RequestReview()
    {
        if (Status.Value == AssignmentStatusEnum.Completed)
            return AssignmentDomainErrors.CannotRequestReviewOnCompleted;

        Status = new AssignmentStatus(AssignmentStatusEnum.WaitingReview);
        RaiseDomainEvent(new AssignmentReviewRequested(Id));

        return Result.Success();
    }

    public Result Complete()
    {
        Status = new AssignmentStatus(AssignmentStatusEnum.Completed);
        RaiseDomainEvent(new AssignmentCompleted(Id));

        return Result.Success();
    }

    public Result Renew()
    {
        Status = new AssignmentStatus(AssignmentStatusEnum.New);
        RaiseDomainEvent(new AssignmentRenewed(Id));

        return Result.Success();
    }

    public Result Update(string title, string description)
    {
        Title = title;
        Description = description;

        RaiseDomainEvent(new AssignmentUpdated(this));

        return Result.Success();
    }
}
