using ProjectManagement.Application.Project.Dtos;

namespace ProjectManagement.Presentation.Project.Endpoints.UpdateProjectPhases;

public record UpdateProjectPhasesRequest(
    string id,
    string ProjectId,
    ICollection<Phase> Phases
);
