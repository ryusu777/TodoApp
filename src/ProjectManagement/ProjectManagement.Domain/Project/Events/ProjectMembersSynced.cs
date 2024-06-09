using Library.Models;
using ProjectManagement.Domain.Common.ValueObjects;

namespace ProjectManagement.Domain.Project.Events;

public record ProjectMembersSynced(
    Project Project, 
    ICollection<UserId> Members,
    ICollection<UserId> DeletedMembers
) : IDomainEvent;
