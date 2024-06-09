using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.Entities;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Commands.CreateProjectHierarchy;

public record CreateProjectHierarchyCommand(
    string ProjectId,
    string Name,
    Guid? SuperiorHierarchyId,
    ICollection<string> MemberUsernames
) : ICommand
{
    public Hierarchy ToDomain()
    {
        return Hierarchy
            .Create(
                Name, 
                SuperiorHierarchyId is null ? null : HierarchyId.Create(SuperiorHierarchyId.Value), 
                MemberUsernames.Select(x => UserId.Create(x)).ToList());
    }
};
