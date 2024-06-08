namespace ProjectManagement.Application.Project.Queries.GetAllProjectMembers;

public record GetAllProjectMembersResult(
    string ProjectId,
    ICollection<string> MemberUsernames
);
