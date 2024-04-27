using AuthContext.Application.Abstractions.Messaging;

namespace AuthContext.Application.User.Commands.ChangePassword;

public record ChangePasswordCommand(
    string Username,
    string OldPassword,
    string NewPassword,
    string VerifyToken
): ICommand;
