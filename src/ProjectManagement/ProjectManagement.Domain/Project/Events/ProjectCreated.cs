using Library.Models;

namespace ProjectManagement.Domain.Project.Events;

public record ProjectCreated(Project Project) : IDomainEvent;