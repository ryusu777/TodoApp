using AuthContext.Application.Abstractions.Messaging;

namespace AuthContext.Application.User.Commands.OnboardUser;

public record OnboardUserCommand(
    string Username, int GiteaUserId, string Email) : ICommand;
