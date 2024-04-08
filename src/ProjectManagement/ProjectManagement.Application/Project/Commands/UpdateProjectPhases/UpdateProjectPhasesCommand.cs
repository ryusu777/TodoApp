using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Project.Dtos;

namespace ProjectManagement.Application.Project.Commands.UpdateProjectPhases;

public record UpdateProjectPhasesCommand(
    string ProjectId, 
    ICollection<Phase> Phases) : ICommand;
