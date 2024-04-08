namespace ProjectManagement.Presentation.Project.Endpoints.UpdateProjectMembers;

public record UpdateProjectMembersRequest(
    string id,
    string ProjectId,
    ICollection<string> MemberUsernames
);
