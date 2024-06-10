namespace ProjectManagement.Presentation.Project;

public static class ProjectEndpointRoutes
{
    public const string Project = "/project";
    public const string DeleteProject = "/project/{ProjectId}";
    public const string ProjectDetail = "/project/{id}";
    public const string GetProjectPages = "/project/pages";
    public const string Members = "/project/{ProjectId}/members";
    public const string SyncMembers = "/project/{id}/sync-members";
    public const string Phases = "/project/{id}/phases";
    public const string Hierarchies = "/project/{id}/hierarchies";
    public const string HierarchyDetail = "/project/{id}/hierarchies/{hierarchyId}";
    public const string HierarchyMembers = "/project/{id}/hierarchies/{hierarchy-id}/members";
    public const string DeleteHierarchy = "/project/{ProjectId}/hierarchies/{HierarchyId}";
    public const string GetAssignableHierarchies = "/project/{ProjectId}/assignable-hierarchies";
}
