using Library.Models;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Project.Entities;

public sealed class Phase : Entity<PhaseId>
{
    public Phase(PhaseId id) : base(id)
    {
    }

    public required string Name { get; set; }
    public required DateOnly StartDate { get; set; }
    public required DateOnly EndDate { get; set; }
    public required string Description { get; set; }
}
