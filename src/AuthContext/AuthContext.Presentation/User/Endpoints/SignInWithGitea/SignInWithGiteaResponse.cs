using AuthContext.Presentation.Common;

namespace AuthContext.Presentation.User.Endpoints.SignInWithGitea;

public record SignInWithGiteaResponse(
    string? ErrorDescription,
    string? ErrorCode,
    string? Data
) : IApiResponse<string>;
