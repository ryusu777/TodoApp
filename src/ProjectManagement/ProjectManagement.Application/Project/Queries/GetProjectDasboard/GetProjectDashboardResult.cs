namespace ProjectManagement.Application.Project.Queries.GetProjectDashboard;

public record GetProjectDashboardResult(
    ICollection<ProjectItem> Projects
);

public record ProjectItem(
    string Code,
    string Name,
    string Description,
    int NumOfOpenAssignment,
    int NumOfWaitingReviewAssignment,
    int NumOfWorkingAssignment,
    int NumOfCompletedAssignment
);
