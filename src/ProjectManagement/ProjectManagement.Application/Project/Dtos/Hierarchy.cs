namespace ProjectManagement.Application.Project.Dtos;

public record Hierarchy(
    Guid Id,
    string Name,
    Guid? SuperiorHierarchyId,
    ICollection<string> MemberUsernames
)
{
    public static Hierarchy? FromDomain(Domain.Project.Entities.Hierarchy? hierarchy)
    {
        if (hierarchy is null)
            return null;
        
        return new Hierarchy(
            hierarchy.Id.Value,
            hierarchy.Name,
            hierarchy.SuperiorHierarchyId?.Value,
            hierarchy.MemberUsernames.Select(x => x.Value).ToList()
        );
    }

};
