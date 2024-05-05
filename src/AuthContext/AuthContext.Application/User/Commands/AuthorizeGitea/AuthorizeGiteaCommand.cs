using AuthContext.Application.Abstractions.Messaging;

namespace AuthContext.Application.User.Commands.AuthorizeGitea;

public record AuthorizeGiteaCommand(string AuthorizationCode) : ICommand<AuthorizeGiteaResult>;
