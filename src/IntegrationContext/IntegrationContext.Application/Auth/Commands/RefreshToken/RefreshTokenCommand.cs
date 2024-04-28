using IntegrationContext.Application.Abstractions.Messaging;

namespace IntegrationContext.Application.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(string Username) : ICommand;
