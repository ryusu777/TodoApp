using Library.Models;

namespace ProjectManagement.Application.Assignment.Events;

public record AssignmentDeleted(Domain.Assignment.Assignment Assignment) : IDomainEvent;
