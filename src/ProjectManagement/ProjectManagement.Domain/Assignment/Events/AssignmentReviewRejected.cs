using Library.Models;
using ProjectManagement.Domain.Assignment.Entities;

namespace ProjectManagement.Domain.Assignment.Events;

public record AssignmentReviewRejected(
    Assignment Assignment,
    Review Review,
    string RejectionNotes) : IDomainEvent;
