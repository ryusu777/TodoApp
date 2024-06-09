using ProjectManagement.Application.Project.Dtos;

namespace ProjectManagement.Application.Project.Queries.GetProjectById;

public record GetProjectByIdResult
(
    string Id,
    string Name,
    string Description,
    int Status,
    ICollection<Phase> ProjectPhases,
    ICollection<Hierarchy> ProjectHierarchies,
    ICollection<string> ProjectMembers,
    int NumOfNewAssignment
);
