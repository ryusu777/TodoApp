using Library.Models;
using ProjectManagement.Domain.Assignment.ValueObjects;

namespace ProjectManagement.Domain.Kanban.Entities;

public class KanbanItem : Entity<AssignmentId>
{
#pragma warning disable CS8618
	private KanbanItem() { }
#pragma warning restore CS8618
	public KanbanItem(
        AssignmentId id,
        int order = 0
    ) : base(id)
    {
        Order = order;
    }

    public int Order { get; set; }
}
