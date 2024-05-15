using Library.Models;
using ProjectManagement.Domain.Common.ValueObjects;

namespace ProjectManagement.Domain.Assignment.Events;

public record AssignmentCreated(
    Domain.Assignment.Assignment Assignment, 
    UserId UserId,
    int GiteaRepositoryId) : IDomainEvent;
