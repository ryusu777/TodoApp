using Library.Models;

namespace ProjectManagement.Domain.Assignment.Events;

public record AssignmentCreated(Assignment Assignment) : IDomainEvent;
