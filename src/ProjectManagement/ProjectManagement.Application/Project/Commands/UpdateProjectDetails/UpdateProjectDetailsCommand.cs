using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Project.Enums;

namespace ProjectManagement.Application.Project.Commands.UpdateProjectDetails;

public record UpdateProjectDetailsCommand(
	string ProjectId, string Name, string Description, ProjectStatus Status
) : ICommand;
