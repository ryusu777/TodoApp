using Library.Models;
using ProjectManagement.Domain.Assignment.Enums;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Common.ValueObjects;

namespace ProjectManagement.Domain.Assignment.Entities;

public class Review : Entity<ReviewId>
{
#pragma warning disable CS8618
	private Review() { }
#pragma warning restore CS8618

    public string Description { get; private set; }
    public string? RejectionNotes { get; private set; }
    public ReviewStatusEnum Status { get; private set; } = ReviewStatusEnum.New;
    public UserId Reviewer { get; private set; }

    private Review(
        ReviewId id,
        string description,
        UserId reviewer) : base(id)
    {
        Description = description;
        Reviewer = reviewer;
    }

    public static Review Create(
        string description,
        UserId reviewer)
    {
        return new Review(
            ReviewId.CreateUnique(),
            description,
            reviewer);
    }

    public Result Reject(string rejectionNotes)
    {
        if (Status != ReviewStatusEnum.New)
        {
            return Result.Failure(AssignmentDomainErrors.CannotReviewACompletedReview);
        }

        Status = ReviewStatusEnum.Rejected;
        RejectionNotes = rejectionNotes;

        return Result.Success();
    }

    public Result Approve()
    {
        if (Status != ReviewStatusEnum.New)
        {
            return Result.Failure(AssignmentDomainErrors.CannotReviewACompletedReview);
        }

        Status = ReviewStatusEnum.Approved;

        return Result.Success();
    }
}
