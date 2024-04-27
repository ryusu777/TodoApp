using AuthContext.Application.Abstractions.Messaging;

namespace AuthContext.Application.User.Commands.SignIn;

public record SignInCommand(string Username, string Password) : ICommand<SignInCommandResult>;

