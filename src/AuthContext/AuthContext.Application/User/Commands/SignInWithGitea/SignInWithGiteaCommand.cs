using AuthContext.Application.Abstractions.Messaging;

namespace AuthContext.Application.User.Commands.SignInWithGitea;

public record SignInWithGiteaCommand() : ICommand<Uri>;
