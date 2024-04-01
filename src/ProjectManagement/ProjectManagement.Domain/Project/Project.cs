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
        RaiseDomainEvent(new ProjectCreated(this));
    }

    public static Project Create(
        string code, 
        string name,
        string description,
        ICollection<UserId> projectMembers,
        ICollection<Phase> projectPhases,
        ProjectStatus status = ProjectStatus.Planning
	) {
        return new Project(
            ProjectId.Create(code),
            name,
            description,
            projectMembers,
            projectPhases,
            status
        );
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public ProjectStatus Status { get; private set; }
    public ICollection<Phase> ProjectPhases { get; private set; }
    public ICollection<UserId> ProjectMembers { get; private set; }

    public void Delete()
    {
        RaiseDomainEvent(new ProjectDeleted(this));
    }
    public Result UpdateProjectMembers(ICollection<UserId> userIds)
    {
        ProjectMembers = userIds;

        RaiseDomainEvent(new ProjectTeamsUpdated(ProjectMembers));

        return Result.Success();
    }

    public Result AddProjectPhase(Phase phase)
    {
        if (ProjectPhases.Any(e => e.Id == phase.Id))
        {
            return ProjectDomainErrors.PhaseAlreadyExists;
        }

        ProjectPhases.Add(phase);

        RaiseDomainEvent(new ProjectPhasesUpdated(ProjectPhases));

        return Result.Success();
    }
    public Result UpdateProjectPhase(PhaseName name, DateOnly start, DateOnly end)
    {
        if (!ProjectPhases.Any(p => p.Id == name))
        {
            return ProjectDomainErrors.PhaseNotFound;
        }

        Phase foundPhase = ProjectPhases.First(p => p.Id == name);

        foundPhase.StartDate = start;
        foundPhase.EndDate = end;

        RaiseDomainEvent(new ProjectPhasesUpdated(ProjectPhases));

        return Result.Success();
    }
    public Result DeleteProjectPhase(PhaseName name) 
    {
        if (!ProjectPhases.Any(p => p.Id == name))
        {
            return ProjectDomainErrors.PhaseNotFound;
        }

        Phase foundPhase = ProjectPhases.First(p => p.Id == name);

        ProjectPhases.Remove(foundPhase);

        RaiseDomainEvent(new ProjectPhasesUpdated(ProjectPhases));

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