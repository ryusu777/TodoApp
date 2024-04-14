using Library.Models;
using ProjectManagement.Domain.Kanban.Entities;
using ProjectManagement.Domain.Kanban.ValueObjects;

namespace ProjectManagement.Domain.Kanban.Events;

public record KanbanItemCreated(KanbanBoardId KanbanBoardId, KanbanItem Item) : IDomainEvent;
