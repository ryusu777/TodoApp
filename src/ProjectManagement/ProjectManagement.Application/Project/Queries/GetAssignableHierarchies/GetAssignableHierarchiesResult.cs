using ProjectManagement.Application.Project.Dtos;

namespace ProjectManagement.Application.Project.Queries.GetAssignableHierarchies;

public record GetAssignableHierarchiesResult(
    string ProjectId,
    ICollection<Hierarchy> Hierarchies
);
