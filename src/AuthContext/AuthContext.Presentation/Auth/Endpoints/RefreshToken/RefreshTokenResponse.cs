using AuthContext.Application.User.Commands.RefreshingToken;
using AuthContext.Presentation.Common;

namespace AuthContext.Presentation.Auth.Endpoints.RefreshToken;

public record RefreshTokenResponse(
    string? ErrorDescription,
    string? ErrorCode,
    RefreshingTokenCommandResult? Data
) : IApiResponse<RefreshingTokenCommandResult>;
