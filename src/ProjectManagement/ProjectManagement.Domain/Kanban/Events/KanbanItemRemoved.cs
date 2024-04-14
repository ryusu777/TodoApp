using Library.Models;
using ProjectManagement.Domain.Kanban.Entities;
using ProjectManagement.Domain.Kanban.ValueObjects;

namespace ProjectManagement.Domain.Kanban.Events;

public record KanbanItemRemoved(KanbanBoardId KanbanBoardId, KanbanItem Item) : IDomainEvent;
