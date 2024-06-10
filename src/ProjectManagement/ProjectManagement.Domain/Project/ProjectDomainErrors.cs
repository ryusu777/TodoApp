using Library.Models;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Project;

public static class ProjectDomainErrors
{
    public static Error PhaseAlreadyExists => new(nameof(PhaseAlreadyExists), "The phase already exists in the project");
    public static Error PhaseNotFound => new(nameof(PhaseNotFound), "The phase is not found");
    public static Error ProjectNotFound => new(nameof(ProjectNotFound), "The project is not found");
    public static Error CannotDeleteProjectWithAssignment => new(nameof(CannotDeleteProjectWithAssignment), "Cannot delete project with assignments, delete the assignments first");
    public static Error FailedToSyncMembers(string message) => new(nameof(FailedToSyncMembers), "Failed to sync project members with Gitea repository: " + message);
    public static Error HierarchyNotFound(HierarchyId id) => new(nameof(HierarchyNotFound), $"The hierarcy {id.Value.ToString()} are not found");
    public static Error HierarchiesAlreadyExists => new(nameof(HierarchiesAlreadyExists), "The hierarchies already exist in the project");
    public static Error CannotDeleteHierarchyWithMembers(HierarchyId id) => new(nameof(CannotDeleteHierarchyWithMembers), $"Cannot delete hierarchy {id.Value} with members");
    public static Error MemberNotFoundInHierarchy(UserId userId, HierarchyId hierarchyId) 
        => new(nameof(MemberNotFoundInHierarchy), $"The member {userId.Value} is not found in the hierarchy {hierarchyId.Value}");
    public static Error UserNotFound(UserId userId) => new(nameof(UserNotFound), $"The user {userId.Value} is not found");
}
