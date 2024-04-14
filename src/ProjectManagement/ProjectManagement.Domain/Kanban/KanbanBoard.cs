using Library.Models;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Kanban.Entities;
using ProjectManagement.Domain.Kanban.Events;
using ProjectManagement.Domain.Kanban.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Kanban;

public class KanbanBoard : AggregateRoot<KanbanBoardId>
{
#pragma warning disable CS8618
	private KanbanBoard() { }
#pragma warning restore CS8618
	private KanbanBoard(KanbanBoardId id, ProjectId projectId) : base(id)
    {
        ProjectId = projectId;
    }

    public static KanbanBoard Create(
        string name,
        SubdomainId subdomainId,
        ProjectId projectId)
    {
        return new(
            KanbanBoardId.Create(name, subdomainId),
            projectId
        );
    }

    public ProjectId ProjectId { get; private set; }
    public ICollection<KanbanItem> Kanbans { get; private set; } = new List<KanbanItem>();

    public Result AddKanban(AssignmentId id) 
    {
        if (Kanbans.Any(e => e.Id == id))
        {
            return KanbanDomainErrors.KanbanAlreadyExists;
        }

        int lastOrder = Kanbans.Max(e => e.Order);
        KanbanItem item = new KanbanItem(id, lastOrder + 1);

        Kanbans.Add(item);

        RaiseDomainEvent(new KanbanItemCreated(Id, item));

        return Result.Success();
    }

    public Result RemoveKanban(AssignmentId id) 
    {
        KanbanItem? found = Kanbans.FirstOrDefault(e => e.Id == id);
        if (found is null)
        {
            return KanbanDomainErrors.KanbanNotFound;
        }

        Kanbans.Remove(found);

        RaiseDomainEvent(new KanbanItemRemoved(Id, found));

        return Result.Success();
    }

    public Result MoveTo(KanbanBoard destinationBoard, AssignmentId kanbanId)
    {
        KanbanItem? found = Kanbans.FirstOrDefault(e => e.Id == kanbanId);
        if (found is null)
        {
            return KanbanDomainErrors.KanbanNotFound;
        }

        RaiseDomainEvent(new KanbanItemMoved(destinationBoard.Id, kanbanId));

        return Result.Success();
    }
}
