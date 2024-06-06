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
        ICollection<UserId> projectMembers,
        ICollection<Phase> projectPhases,
        ProjectStatus status = ProjectStatus.Planning
    ) : base(code)
    {
        Name = name;
        Description = description;
        ProjectMembers = projectMembers;
        Hierarchies = projectHierarchies;
        ProjectPhases = projectPhases;
        Status = status;
    }

    public static Project Create(
        string code, 
        string name,
        string description,
        ICollection<UserId> projectMembers,
        ICollection<Phase> projectPhases,
        ProjectStatus status = ProjectStatus.Planning
	) {
        var result = new Project(
            ProjectId.Create(code),
            name,
            description,
            new List<Hierarchy>(),
            projectMembers,
            projectPhases,
            status
        );

        return result;
    }

    public static Project Create(
        string code, 
        string name,
        string description,
        ICollection<Hierarchy> projectHierarchies,
        ICollection<UserId> projectMembers,
        ICollection<Phase> projectPhases,
        ProjectStatus status = ProjectStatus.Planning
	) {
        var result = new Project(
            ProjectId.Create(code),
            name,
            description,
            projectHierarchies,
            projectMembers,
            projectPhases,
            status
        );

        return result;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public ProjectStatus Status { get; private set; }
    public ICollection<Phase> ProjectPhases { get; private set; }
    public ICollection<UserId> ProjectMembers { get; private set; }
    public ICollection<Hierarchy> Hierarchies { get; private set; }

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
        return Hierarchies.SelectMany(x => x.MemberUsernames).ToList();
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

    /*
     * This method is deprecated, use Project Hierarchy for members
     * */
    public Result UpdateProjectMembers(ICollection<UserId> userIds)
    {
        ProjectMembers = userIds;

        RaiseDomainEvent(new ProjectMembersUpdated(Id, ProjectMembers));

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
}
