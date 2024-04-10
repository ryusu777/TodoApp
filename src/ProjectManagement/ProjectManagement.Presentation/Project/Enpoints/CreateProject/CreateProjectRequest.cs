using ProjectManagement.Application.Project.Commands.CreateProject;
using ProjectManagement.Application.Project.Commands.CreateProjectPhase;

namespace ProjectManagement.Presentation.Project.Endpoints.CreateProject;

public record CreateProjectRequest : CreateProjectCommand
{
	public CreateProjectRequest(string Code, string Name, string Description, ICollection<string> ProjectMembers, ICollection<CreateProjectPhaseCommand> ProjectPhases) : base(Code, Name, Description, ProjectMembers, ProjectPhases)
	{
	}
}
