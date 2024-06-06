using Library.Models;
using ProjectManagement.Domain.Project.Entities;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Project.Events;

public record ProjectHierarchyCreated(
    ProjectId ProjectId,
    Hierarchy Hierarchy) : IDomainEvent;
