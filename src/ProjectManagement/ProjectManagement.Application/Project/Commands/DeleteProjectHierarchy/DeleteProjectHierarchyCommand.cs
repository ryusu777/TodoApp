using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Project.Commands.DeleteProjectHierarchy;

public record DeleteProjectHierarchyCommand(string ProjectId, Guid HierarchyId) : ICommand;
