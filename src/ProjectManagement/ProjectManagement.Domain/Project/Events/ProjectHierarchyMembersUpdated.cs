using Library.Models;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Project.Events;

public record ProjectHierarchyMembersUpdated(
    ProjectId ProjectId,
    HierarchyId HierarchyId,
    ICollection<UserId> Members
) : IDomainEvent;
