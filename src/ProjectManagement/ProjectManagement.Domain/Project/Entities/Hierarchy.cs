using Library.Models;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Project.Entities;

public class Hierarchy : Entity<HierarchyId>
{
#pragma warning disable CS8618
	private Hierarchy() { }
#pragma warning restore CS8618

    public string Name { get; private set; }
    public HierarchyId? SuperiorHierarchyId { get; private set; }
    public ICollection<UserId> MemberUsernames { get; private set; }

    private Hierarchy(
        HierarchyId id,
        string name,
        HierarchyId? superiorHierarchyId,
        ICollection<UserId>? memberUsernames) : base(id)
    {
        Name = name;
        SuperiorHierarchyId = superiorHierarchyId;
        MemberUsernames = memberUsernames ?? new List<UserId>();
    }

    public static Hierarchy Create(
        string name,
        HierarchyId? superiorHierarchyId,
        ICollection<UserId>? memberUsernames)
    {
        var result = new Hierarchy(
            HierarchyId.CreateUnique(),
            name,
            superiorHierarchyId,
            memberUsernames
        );

        return result;
    }

    public void UpdateMembers(ICollection<UserId> memberUsernames)
    {
        MemberUsernames = memberUsernames;
    }

    public void UpdateDetails(
        string name,
        HierarchyId? superiorHierarchyId)
    {
        Name = name;
        SuperiorHierarchyId = superiorHierarchyId;
    }
}
