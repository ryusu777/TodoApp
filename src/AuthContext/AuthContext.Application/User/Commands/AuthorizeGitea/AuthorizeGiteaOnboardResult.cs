namespace AuthContext.Application.User.Commands.AuthorizeGitea;

public record AuthorizeGiteaOnboardResult(
    string PasswordChangeToken,
    string Username,
    string Email
);
