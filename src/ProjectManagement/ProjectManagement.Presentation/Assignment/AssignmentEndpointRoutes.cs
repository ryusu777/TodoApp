namespace ProjectManagement.Presentation.Assignment;

public static class AssignmentEndpointRoutes
{
    public const string Assignments = "/project/{project_id}/assignment/";
    public const string GetAssignments = "/project/{ProjectId}/assignments/{SubdomainId?}";
    public const string DeleteAssignment = "/assignment/{AssignmentId}";
    public const string AssignmentDetail = "/assignment/{assignment_id}";
    public const string Assigning = "/assignment/{assignment_id}/assign";
    public const string RemoveAssignee = "/assignment/{assignment_id}/remove-assignee";
    public const string AssignmentStatus = "/assignment/{assignment_id}/status";
}
