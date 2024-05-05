using AuthContext.Application.Identity.Model;

namespace AuthContext.Application.User.Commands.AuthorizeGitea;

public record AuthorizeGiteaResult(
    AuthenticationResult? AuthResult,
    AuthorizeGiteaOnboardResult? OnboardInformation
);
