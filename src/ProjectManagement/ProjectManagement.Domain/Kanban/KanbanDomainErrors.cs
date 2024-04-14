using Library.Models;

namespace ProjectManagement.Domain.Kanban;

public static class KanbanDomainErrors 
{
	public static Error KanbanAlreadyExists => new(nameof(KanbanAlreadyExists), "The kanban already exist in the board");
	public static Error KanbanNotFound => new(nameof(KanbanNotFound), "The kanban is not found");
}
