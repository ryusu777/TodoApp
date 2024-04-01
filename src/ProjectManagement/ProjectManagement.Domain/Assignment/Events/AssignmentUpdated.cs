using Library.Models;

namespace ProjectManagement.Domain.Assignment.Events;

public record AssignmentUpdated(Assignment Assignment) : IDomainEvent;
