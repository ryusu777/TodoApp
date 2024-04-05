using Library.Models;
using ProjectManagement.Domain.Project.Entities;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Project.Events;

public record ProjectPhasesUpdated(ProjectId ProjectId, ICollection<Phase> Phases) : IDomainEvent;