using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.Entities;

namespace ProjectManagement.Application.Project.Commands.CreateProject;

public record CreateProjectCommand(
	string Code,
	string Name,
	string Description,
	ICollection<UserId> ProjectMembers,
	ICollection<Phase> ProjectPhases
) : ICommand;
