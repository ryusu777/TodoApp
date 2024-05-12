namespace AuthContext.Presentation.Auth;

public static class AuthEndpointRoutes
{
    public const string SignIn = "/auth/sign-in";
    public const string RefreshToken = "/auth/refresh-token";
    public const string SignInWithGitea = "/auth/sign-in-gitea";
    public const string AuthorizeGitea = "/auth/authorize-gitea";
    public const string OnboardUser = "/auth/onboard-user";
}
