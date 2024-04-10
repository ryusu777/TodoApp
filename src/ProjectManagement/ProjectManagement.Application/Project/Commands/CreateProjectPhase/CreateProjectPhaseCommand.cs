using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Commands.CreateProjectPhase;

public record CreateProjectPhaseCommand(
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    string Description
) : ICommand
{
    public Domain.Project.Entities.Phase ToDomain()
    {
        return new Domain.Project.Entities.Phase(PhaseId.CreateUnique())
        {
            Name = Name,
            StartDate = StartDate,
            EndDate = EndDate,
            Description = Description
        };
    }
}
