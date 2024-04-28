using IntegrationContext.Application.Abstractions.Messaging;

namespace IntegrationContext.Application.Auth.Commands.GrantAccessToken;

public record GrantAccessTokenCommand(
    string Username, string GrantCode) : ICommand;
