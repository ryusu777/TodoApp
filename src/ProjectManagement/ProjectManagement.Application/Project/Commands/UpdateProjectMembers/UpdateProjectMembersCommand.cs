using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Project.Commands.UpdateProjectMembers;

public record UpdateProjectMembersCommand(string ProjectId, ICollection<string> MemberUsernames) : ICommand;
