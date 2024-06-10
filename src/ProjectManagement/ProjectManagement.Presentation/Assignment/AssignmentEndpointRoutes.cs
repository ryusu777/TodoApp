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

    public const string WorkOnAssignment = "/assignment/{assignment_id}/work-on";
    public const string RequestAssignmentReview = "/assignment/{assignment_id}/request-review";
    public const string ApproveAssignmentReview = "/assignment/{assignment_id}/approve-review";
    public const string RejectAssignmentReview = "/assignment/{assignment_id}/reject-review";
    public const string ReopenAssignment = "/assignment/{assignment_id}/reopen";
}
