using Library.Models;
using ProjectManagement.Domain.Common.ValueObjects;

namespace ProjectManagement.Domain.Project.Events;

public record ProjectTeamsUpdated(ICollection<UserId> Users) : IDomainEvent;