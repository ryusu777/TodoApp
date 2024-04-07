using Library.Models;

namespace ProjectManagement.Application.Assignment.Events;

public record AssignmentCreated(Domain.Assignment.Assignment Assignment) : IDomainEvent;
