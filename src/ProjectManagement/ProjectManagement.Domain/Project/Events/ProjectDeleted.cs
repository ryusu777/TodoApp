using Library.Models;

namespace ProjectManagement.Domain.Project.Events;

public record ProjectDeleted(Project Project) : IDomainEvent;