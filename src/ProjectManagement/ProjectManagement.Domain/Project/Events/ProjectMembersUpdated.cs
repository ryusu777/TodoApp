using Library.Models;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Project.Events;

public record ProjectMembersUpdated(ProjectId ProjectId, ICollection<UserId> Users) : IDomainEvent;