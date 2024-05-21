using Library.Models;
using ProjectManagement.Domain.Assignment.ValueObjects;

namespace ProjectManagement.Domain.Assignment.Events;

public record AssignmentRenewedFromHook(AssignmentId Id) : IDomainEvent;
