using Library.Models;

namespace ProjectManagement.Application.Project.Events;

public record ProjectDeleted(Domain.Project.Project Project) : IDomainEvent;
