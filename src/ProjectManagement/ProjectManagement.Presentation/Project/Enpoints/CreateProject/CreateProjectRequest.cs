using ProjectManagement.Application.Project.Commands.CreateProject;
using ProjectManagement.Application.Project.Dtos;

namespace ProjectManagement.Presentation.Project.Endpoints.CreateProject;

public record CreateProjectRequest : CreateProjectCommand
{
    public CreateProjectRequest(string Code, string Name, string Description, ICollection<string> ProjectMembers, ICollection<Phase> ProjectPhases) : base(Code, Name, Description, ProjectMembers, ProjectPhases)
    {
    }
}
