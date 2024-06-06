using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Project.Commands.UpdateProjectHierarchyMembers;

public record UpdateProjectHierarchyMembersCommand(
    string ProjectId,
    Guid HierarchyId,
    ICollection<string> MemberUsernames
) : ICommand;
