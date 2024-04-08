using ProjectManagement.Application.Project.Commands.UpdateProjectMembers;

namespace ProjectManagement.Presentation.Project.Endpoints.UpdateProjectMembers;

public record UpdateProjectMembersRequest : UpdateProjectMembersCommand
{
    public required string id { get; init; }
    public UpdateProjectMembersRequest(string ProjectId, ICollection<string> MemberUsernames) : base(ProjectId, MemberUsernames)
    {
    }
}
