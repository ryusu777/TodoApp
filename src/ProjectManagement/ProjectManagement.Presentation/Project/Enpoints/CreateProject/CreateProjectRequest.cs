using ProjectManagement.Application.Project.Dtos;

namespace ProjectManagement.Presentation.Project.Endpoints.CreateProject;

public record CreateProjectRequest(
    string Code, 
    string Name, 
    string Description, 
    ICollection<string> ProjectMembers, 
    ICollection<Phase> ProjectPhases
);
