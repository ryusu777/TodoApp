using Library.Models;
using ProjectManagement.Domain.Project.Entities;

namespace ProjectManagement.Domain.Project.Events;

public record ProjectPhasesUpdated(ICollection<Phase> Phases) : IDomainEvent;