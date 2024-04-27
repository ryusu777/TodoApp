using AuthContext.Application.Abstractions.Messaging;

namespace AuthContext.Application.User.Commands.RequestChangePassword;

public record RequestChangePasswordCommand(string Username) : ICommand;
