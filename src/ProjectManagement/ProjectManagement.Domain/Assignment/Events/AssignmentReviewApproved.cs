using Library.Models;
using ProjectManagement.Domain.Assignment.Entities;

namespace ProjectManagement.Domain.Assignment.Events;

public record AssignmentReviewApproved(
    Assignment Assignment,
    Review Review
) : IDomainEvent;
