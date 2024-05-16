using Library.Models;
using ProjectManagement.Domain.Common.ValueObjects;

namespace ProjectManagement.Domain.Assignment.Events;

public record AssignmentUpdated(Assignment Assignment, UserId UserId) : IDomainEvent;
