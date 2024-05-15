using Library.Models;

namespace ProjectManagement.Domain.Assignment.Events;

public record AssignmentDeleted(Domain.Assignment.Assignment Assignment) : IDomainEvent;
