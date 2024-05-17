using Library.Models;

namespace ProjectManagement.Domain.Assignment.Events;

public record AssignmentCreatedFromHook(
    Domain.Assignment.Assignment Assignment) : IDomainEvent;
