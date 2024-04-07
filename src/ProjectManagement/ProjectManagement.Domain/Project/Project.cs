using Library.Models;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.Entities;
using ProjectManagement.Domain.Project.Enums;
using ProjectManagement.Domain.Project.Events;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Project;

public sealed class Project : AggregateRoot<ProjectId>
{
    private Project(
        ProjectId code,
        string name,
        string description,
        ICollection<UserId> projectMembers,
        ICollection<Phase> projectPhases,
        ProjectStatus status = ProjectStatus.Planning
    ) : base(code)
    {
        Name = name;
        Description = description;
        ProjectMembers = projectMembers;
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
