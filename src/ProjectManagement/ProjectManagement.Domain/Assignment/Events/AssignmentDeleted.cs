using Library.Models;
using ProjectManagement.Domain.Common.ValueObjects;

namespace ProjectManagement.Domain.Assignment.Events;

public record AssignmentDeleted(Domain.Assignment.Assignment Assignment, UserId UserId) : IDomainEvent;
