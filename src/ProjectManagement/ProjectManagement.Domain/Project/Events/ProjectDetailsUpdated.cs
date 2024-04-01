using Library.Models;

namespace ProjectManagement.Domain.Project.Events;

public record ProjectDetailsUpdated(Project Project) : IDomainEvent;