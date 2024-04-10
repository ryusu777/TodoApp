using Library.Models;
using MediatR;

namespace ProjectManagement.Application.Project.Events;

public record ProjectCreated(Domain.Project.Project Project) : IDomainEvent;
