using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Dtos;

public record Phase(
    Guid? Id,
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    string Description
) 
{
    public Domain.Project.Entities.Phase ToDomain()
    {
        return new Domain.Project.Entities.Phase(Id is not null 
            ? PhaseId.Create(Id.Value)
            : PhaseId.CreateUnique())
        {
            Name = Name,
            StartDate = StartDate,
            EndDate = EndDate,
            Description = Description
        };
    }

    public static Phase FromDomain(Domain.Project.Entities.Phase phase)
    {
        return new Phase(
            phase.Id.Value,
            phase.Name,
            phase.StartDate,
            phase.EndDate,
            phase.Description
        );
    }
};
