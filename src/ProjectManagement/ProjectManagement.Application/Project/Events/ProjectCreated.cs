using Library.Models;

namespace ProjectManagement.Application.Project.Events;

public record ProjectCreated(Domain.Project.Project Project) : IDomainEvent;
