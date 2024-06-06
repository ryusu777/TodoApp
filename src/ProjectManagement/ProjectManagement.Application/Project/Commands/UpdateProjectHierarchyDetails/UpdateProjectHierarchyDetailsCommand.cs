using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Project.Commands.UpdateProjectHierarchyDetails;

public record UpdateProjectHierarchyDetailsCommand(
    string ProjectId,
    Guid HierarchyId,
    string Name,
    Guid? SuperiorHierarchyId
) : ICommand;
