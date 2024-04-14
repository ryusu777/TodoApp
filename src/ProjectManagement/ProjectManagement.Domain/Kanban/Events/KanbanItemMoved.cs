using Library.Models;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Kanban.ValueObjects;

namespace ProjectManagement.Domain.Kanban.Events;

public record KanbanItemMoved(KanbanBoardId DestinationId, AssignmentId KanbanId) : IDomainEvent;
