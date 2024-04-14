using Library.Models;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Kanban.ValueObjects;

public sealed class KanbanBoardId : ValueObject
{
    public string Name { get; init; }
    public SubdomainId SubdomainId { get; init; }
    private KanbanBoardId(string name, SubdomainId subdomainId) 
    {
        Name = name;
        SubdomainId = subdomainId;
    }
    public static KanbanBoardId Create(string name, SubdomainId subdomainId)
    {
        return new(name, subdomainId);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return SubdomainId;
    }
}
