using System.Security.Cryptography.X509Certificates;
using Library.Models;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Project.Entities;

public sealed class Phase : Entity<PhaseName>
{
    public Phase(PhaseName name) : base(name)
    {
    }

    public required DateOnly StartDate { get; set; }
    public required DateOnly EndDate { get; set; }
    public required string Description { get; set; }
}