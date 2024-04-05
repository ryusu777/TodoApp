using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Commands.DeleteProject;

public record DeleteProjectCommand(string ProjectId) : ICommand;
