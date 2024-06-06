using Library.Models;
using ProjectManagement.Domain.Project.Entities;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Project.Events;

public record ProjectHierarchyDetailUpdated(
    ProjectId ProjectId,
    Hierarchy Hierarchy) : IDomainEvent;
