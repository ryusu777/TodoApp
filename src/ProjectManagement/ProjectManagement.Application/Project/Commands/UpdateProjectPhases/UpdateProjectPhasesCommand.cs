using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Project.Entities;

namespace ProjectManagement.Application.Project.Commands.UpdateProjectPhases;

public record UpdateProjectPhasesCommand(
    string ProjectId, 
    ICollection<Phase> Phases) : ICommand;
