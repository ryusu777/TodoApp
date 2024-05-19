using Library.Models;

namespace ProjectManagement.Domain.Assignment.Events;

public record AssignmentUpdatedFromHook(Assignment Assignment) : IDomainEvent;
