using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Dtos;

public record Phase(
    Guid Id,
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    string Description
) 
{
    public Domain.Project.Entities.Phase ToDomain()
    {
        return new Domain.Project.Entities.Phase(PhaseId.Create(Id))
        {
            Name = Name,
            StartDate = StartDate,
            EndDate = EndDate,
            Description = Description
        };
    }
};
