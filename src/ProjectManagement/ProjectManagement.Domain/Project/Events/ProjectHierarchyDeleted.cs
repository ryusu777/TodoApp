using Library.Models;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Project.Events;

public record ProjectHierarchyDeleted(
    ProjectId ProjectId, 
    HierarchyId HierarchyId) : IDomainEvent;
