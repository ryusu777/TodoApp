using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Project.Dtos;

namespace ProjectManagement.Application.Project.Commands.CreateProject;

public record CreateProjectCommand(
	string Code,
	string Name,
	string Description,
	ICollection<string> ProjectMembers,
	ICollection<Phase> ProjectPhases
) : ICommand;
