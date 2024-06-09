using Library.Models;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.Entities;
using ProjectManagement.Domain.Project.Enums;
using ProjectManagement.Domain.Project.Events;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Project;

public sealed class Project : AggregateRoot<ProjectId>
{
#pragma warning disable CS8618
	private Project() { }
#pragma warning restore CS8618

	private Project(
        ProjectId code,
        string name,
        string description,
        ICollection<Hierarchy> projectHierarchies,
        ICollection<Phase> projectPhases,
        ProjectStatus status = ProjectStatus.Planning
    ) : base(code)
    {
        Name = name;
        Description = description;
        Hierarchies = projectHierarchies;
        ProjectPhases = projectPhases;
        Status = status;
    }

    public static Project Create(
        string code, 
        string name,
        string description,
        ICollection<Hierarchy> projectHierarchies,
        ICollection<Phase> projectPhases,
        ProjectStatus status = ProjectStatus.Planning
	) {
        var result = new Project(
            ProjectId.Create(code),
            name,
            description,
            projectHierarchies,
            projectPhases,
            status
        );

        return result;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public ProjectStatus Status { get; private set; }
    public ICollection<Phase> ProjectPhases { get; private set; }
    public ICollection<Hierarchy> Hierarchies { get; private set; }
    public ICollection<UserId> Members { get; private set; } = new List<UserId>();

    public Result UpdateProjectHierarchyMembers(HierarchyId id, ICollection<UserId> memberUserNames)
    {
        var hierarchy = Hierarchies.FirstOrDefault(x => x.Id == id);

        if (hierarchy is null)
        {
            return Result.Failure(ProjectDomainErrors.HierarchyNotFound(id));
        }

        hierarchy.UpdateMembers(memberUserNames);

        RaiseDomainEvent(new ProjectHierarchyMembersUpdated(Id, id, memberUserNames));

        return Result.Success();
    }

    public List<UserId> GetAllProjectMembers()
    {
        return Hierarchies.SelectMany(x => x.MemberUsernames).Distinct().ToList();
    }

    public Result SyncProjectMembers(ICollection<UserId> usernames)
    {
        Members = usernames;
        RaiseDomainEvent(new ProjectMembersSynced(Id, usernames));
        return Result.Success();
    }

    public Result UpdateProjectHierarchyDetail(HierarchyId id, string name, HierarchyId? superiorHierarchyId)
    {
        var hierarchy = Hierarchies.FirstOrDefault(x => x.Id == id);

        if (hierarchy is null)
        {
            return Result.Failure(ProjectDomainErrors.HierarchyNotFound(id));
        }

        hierarchy.UpdateDetails(name, superiorHierarchyId);

        RaiseDomainEvent(new ProjectHierarchyDetailUpdated(Id, hierarchy));

        return Result.Success();
    }

    public Result AddProjectHierarchy(Hierarchy hierarchy)
    {
        Hierarchies.Add(hierarchy);

        RaiseDomainEvent(new ProjectHierarchyCreated(Id, hierarchy));

        return Result.Success();
    }

    public Result DeleteProjectHierarchy(HierarchyId id)
    {
        var hierarchy = Hierarchies.FirstOrDefault(x => x.Id == id);

        if (hierarchy is null)
        {
            return Result.Failure(ProjectDomainErrors.HierarchyNotFound(id));
        }

        if (hierarchy.MemberUsernames.Any())
        {
            return Result.Failure(ProjectDomainErrors.CannotDeleteHierarchyWithMembers(id));
        }

        Hierarchies.Remove(hierarchy);

        RaiseDomainEvent(new ProjectHierarchyDeleted(Id, id));

        return Result.Success();
    }

    public Result UpdateProjectPhases(ICollection<Phase> phases)
    {
        ProjectPhases = phases;

        RaiseDomainEvent(new ProjectPhasesUpdated(Id, ProjectPhases));

        return Result.Success();
    }

    public Result UpdateProject(string name, string description, ProjectStatus status)
    {
        Name = name;
        Description = description;
        Status = status;

        RaiseDomainEvent(new ProjectDetailsUpdated(this));

        return Result.Success();
    }

    public Result<List<Hierarchy>> GetAssignableHierarchies(UserId userId)
    {
        // select member from subordinates hierarchies
        var hierarchy = Hierarchies.Where(x => x.MemberUsernames.Any(e => e == userId)).ToList();

        if (hierarchy.Count == 0)
        {
            return Result.Failure<List<Hierarchy>>(ProjectDomainErrors.UserNotFound(userId));
        }

        var subordinates = new List<Hierarchy>();

        foreach (var h in hierarchy)
        {
            subordinates.AddRange(GetSubordinateHierarchy(h, new List<Hierarchy>()));
        }

        return Result.Success(subordinates);
    }

    private List<Hierarchy> GetSubordinateHierarchy(Hierarchy hierarchy, List<Hierarchy> visitedHierarchies)
    {
        if (visitedHierarchies.Contains(hierarchy))
        {
            return visitedHierarchies;
        }

        foreach (var subHierarchy in Hierarchies.Where(x => x.SuperiorHierarchyId == hierarchy.Id))
        {
            visitedHierarchies = GetSubordinateHierarchy(subHierarchy, visitedHierarchies);
            visitedHierarchies.Add(subHierarchy);
        }

        return visitedHierarchies;
    }
}
