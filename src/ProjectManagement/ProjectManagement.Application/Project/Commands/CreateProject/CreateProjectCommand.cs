using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Project.Commands.CreateProjectHierarchy;
using ProjectManagement.Application.Project.Commands.CreateProjectPhase;

namespace ProjectManagement.Application.Project.Commands.CreateProject;

public record CreateProjectCommand(
	string Code,
	string Name,
	string Description,
	ICollection<CreateProjectHierarchyCommand> ProjectHierarchies,
	ICollection<CreateProjectPhaseCommand> ProjectPhases
) : ICommand;
