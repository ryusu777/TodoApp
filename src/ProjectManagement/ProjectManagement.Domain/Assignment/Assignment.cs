using Library.Models;
using ProjectManagement.Domain.Assignment.Entities;
using ProjectManagement.Domain.Assignment.Enums;
using ProjectManagement.Domain.Assignment.Events;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Assignment;

public sealed class Assignment : AggregateRoot<AssignmentId>
{
#pragma warning disable CS8618
	private Assignment() { }
#pragma warning restore CS8618
	private Assignment(
        AssignmentId id, 
        string title, 
        string description, 
        ProjectId projectId,
        ICollection<UserId> assignees,
        SubdomainId? subdomainId,
        DateTime? deadline = null,
        PhaseId? phaseId = null,
        UserId? reviewer = null) : base(id)
    {
        Title = title;
        Description = description;
        ProjectId = projectId;
        Status = new AssignmentStatus(AssignmentStatusEnum.New);
        SubdomainId = subdomainId;
        PhaseId = phaseId;
        Reviewer = reviewer;
        Deadline = deadline;
        Assignees = assignees;
    }

    public string Title { get; private set; }
    public string? Description { get; private set; }
    public ProjectId ProjectId { get; private set; }
    public AssignmentStatus Status { get; private set; }
    public SubdomainId? SubdomainId { get; private set; }
    public PhaseId? PhaseId { get; private set; }
    public ICollection<UserId> Assignees { get; private set; } = new List<UserId>();
    public UserId? Reviewer { get; private set; }
    public DateTime? Deadline { get; private set; }
    public ICollection<Review> Reviews { get; private set; } = new List<Review>();

    public static Assignment Create(
        string title, 
        string description, 
        ProjectId projectId,
        ICollection<UserId> assignees,
        SubdomainId? subdomainId = null,
        DateTime? deadline = null,
        PhaseId? phaseId = null,
        UserId? reviewer = null)
    {
        var result = new Assignment(
            AssignmentId.CreateUnique(),
            title,
            description,
            projectId,
            assignees,
            subdomainId,
            deadline,
            phaseId,
            reviewer
		);
        return result;
    }
    
    public static Assignment Create(
        AssignmentId id,
        string title, 
        string description, 
        ProjectId projectId,
        ICollection<UserId> assignees,
        SubdomainId? subdomainId = null,
        DateTime? deadline = null,
        PhaseId? phaseId = null,
        UserId? reviewer = null)
    {
        var result = new Assignment(
            id,
            title,
            description,
            projectId,
            assignees,
            subdomainId,
            deadline,
            phaseId,
            reviewer
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
        Status = new AssignmentStatus(AssignmentStatusEnum.OnProgress);
        RaiseDomainEvent(new AssignmentWorkedOn(Id));

        return Result.Success();
    }

    public Result RequestReview(string description)
    {
        if (Reviewer is null)
        {
            return Result.Failure(AssignmentDomainErrors.CannotRequestReviewWithEmptyReviewer);
        }
        Status = new AssignmentStatus(AssignmentStatusEnum.WaitingReview);
        Reviews.Add(Review.Create(description, Reviewer));
        RaiseDomainEvent(new AssignmentReviewRequested(Id));

        return Result.Success();
    }

    public Result ApproveCompletion()
    {
        if (Status != AssignmentStatusEnum.WaitingReview)
            return AssignmentDomainErrors.CannotReviewANonWaitingReviewAssignment;

        Status = new AssignmentStatus(AssignmentStatusEnum.Completed);
        var currentReview = Reviews.OrderBy(e => e.CreatedAt).FirstOrDefault();

        if (currentReview is null)
            return AssignmentDomainErrors.TheAssignmentDoesNotHaveReviewRequest;

        currentReview.Approve();
        RaiseDomainEvent(new AssignmentReviewApproved(this, currentReview));

        return Result.Success();
    }

    public Result RejectCompletion(string rejectionNotes)
    {
        if (Status != AssignmentStatusEnum.WaitingReview)
            return AssignmentDomainErrors.CannotReviewANonWaitingReviewAssignment;

        Status = new AssignmentStatus(AssignmentStatusEnum.Revised);
        var currentReview = Reviews.OrderBy(e => e.CreatedAt).FirstOrDefault();

        if (currentReview is null)
            return AssignmentDomainErrors.TheAssignmentDoesNotHaveReviewRequest;

        currentReview.Reject(rejectionNotes);
        RaiseDomainEvent(new AssignmentReviewRejected(this, currentReview, rejectionNotes));

        return Result.Success();
    }

    public Result Complete(UserId userId)
    {
        Status = new AssignmentStatus(AssignmentStatusEnum.Completed);
        RaiseDomainEvent(new AssignmentCompleted(Id, userId));

        return Result.Success();
    }

    public Result CompleteFromHook()
    {
        Status = new AssignmentStatus(AssignmentStatusEnum.Completed);
        RaiseDomainEvent(new AssignmentCompletedFromHook(Id));

        return Result.Success();
    }

    public Result Renew(UserId userId)
    {
        Status = new AssignmentStatus(AssignmentStatusEnum.New);
        RaiseDomainEvent(new AssignmentRenewed(Id, userId));

        return Result.Success();
    }

    public Result RenewFromHook()
    {
        Status = new AssignmentStatus(AssignmentStatusEnum.New);
        RaiseDomainEvent(new AssignmentRenewedFromHook(Id));

        return Result.Success();
    }

    public Result Update(
        UserId userId,
        string title, 
        string description,
        ICollection<UserId>? assignees,
        SubdomainId? subdomainId = null,
        PhaseId? phaseId = null,
        UserId? reviewer = null
    )
    {
        Title = title;
        Description = description;

        if (subdomainId is not null)
            SubdomainId = subdomainId;

        if (phaseId is not null)
            PhaseId = phaseId;

        if (reviewer is not null)
            Reviewer = reviewer;

        if (assignees is not null)
            Assignees = assignees;

        RaiseDomainEvent(new AssignmentUpdated(this, userId));

        return Result.Success();
    }
    
    public Result UpdateFromHook(
        string title, 
        string description,
        ICollection<UserId>? assignees
    )
    {
        Title = title;
        Description = description;

        if (assignees is not null)
            Assignees = assignees;

        RaiseDomainEvent(new AssignmentUpdatedFromHook(this));

        return Result.Success();
    }
}
