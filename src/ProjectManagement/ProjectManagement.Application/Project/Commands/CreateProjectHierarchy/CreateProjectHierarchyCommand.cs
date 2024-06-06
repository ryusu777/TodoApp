using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Project.Commands.CreateProjectHierarchy;

public record CreateProjectHierarchyCommand(
    string ProjectId,
    string Name,
    Guid? SuperiorId,
    ICollection<string> MemberUsernames
) : ICommand;
