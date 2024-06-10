namespace ProjectManagement.Application.Assignment.Dtos;

public record Review(
    Guid Id,
    string Description,
    string? RejectionNotes,
    string Status,
    string Reviewer
)
{
    public static Review? FromDomain(Domain.Assignment.Entities.Review? review)
    {
        if (review is null)
            return null;
        return new Review(
            review.Id.Value,
            review.Description,
            review.RejectionNotes,
            review.Status.ToString(),
            review.Reviewer.Value
        );
    }
}
