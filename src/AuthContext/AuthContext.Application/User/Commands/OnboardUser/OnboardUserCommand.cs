using AuthContext.Application.Abstractions.Messaging;

namespace AuthContext.Application.User.Commands.OnboardUser;

public record OnboardUserCommand(
    string Username, 
    string Email,
    string Password,
    string ChangePasswordCode
) : ICommand;
