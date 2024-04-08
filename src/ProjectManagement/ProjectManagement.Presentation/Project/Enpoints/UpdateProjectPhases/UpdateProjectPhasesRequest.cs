using ProjectManagement.Application.Project.Commands.UpdateProjectPhases;
using ProjectManagement.Application.Project.Dtos;

namespace ProjectManagement.Presentation.Project.Endpoints.UpdateProjectPhases;

public record UpdateProjectPhasesRequest : UpdateProjectPhasesCommand
{
    public required string id { get; init; }
    public UpdateProjectPhasesRequest(string ProjectId, ICollection<Phase> Phases) : base(ProjectId, Phases)
    {
    }
}
