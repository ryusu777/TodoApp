using Library.Models;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Common.ValueObjects;

namespace ProjectManagement.Domain.Assignment.Events;

public record AssignmentRemoved(AssignmentId Id, UserId Assignees) : IDomainEvent;
