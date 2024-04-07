using Library.Models;
using ProjectManagement.Domain.Assignment.ValueObjects;

namespace ProjectManagement.Application.Assignment.Events;

public record AssignmentDeleted(AssignmentId Id) : IDomainEvent;
