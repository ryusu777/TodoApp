using AuthContext.Application.User.Commands.AuthorizeGitea;
using AuthContext.Presentation.Common;

namespace AuthContext.Presentation.Auth.Endpoints.AuthorizeGitea;

public record AuthorizeGiteaResponse(
    string? ErrorDescription,
    string? ErrorCode,
    AuthorizeGiteaResult? Data
) : IApiResponse<AuthorizeGiteaResult>;
