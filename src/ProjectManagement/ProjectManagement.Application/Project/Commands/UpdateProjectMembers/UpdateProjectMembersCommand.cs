using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Common.ValueObjects;

namespace ProjectManagement.Application.Project.Commands.UpdateProjectMembers;

public record UpdateProjectMembersCommand(string ProjectId, ICollection<UserId> Users) : ICommand;
