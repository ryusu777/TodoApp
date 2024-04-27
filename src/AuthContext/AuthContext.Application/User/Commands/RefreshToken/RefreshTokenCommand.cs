using AuthContext.Application.Abstractions.Messaging;

namespace AuthContext.Application.User.Commands.RefreshingToken;

public record RefreshingTokenCommand(
    string JwtToken,
    string RefreshToken
) : ICommand<RefreshingTokenCommandResult>;
