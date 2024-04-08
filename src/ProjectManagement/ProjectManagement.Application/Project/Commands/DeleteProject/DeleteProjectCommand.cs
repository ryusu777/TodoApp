using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Project.Commands.DeleteProject;

public record DeleteProjectCommand(string ProjectId) : ICommand;
