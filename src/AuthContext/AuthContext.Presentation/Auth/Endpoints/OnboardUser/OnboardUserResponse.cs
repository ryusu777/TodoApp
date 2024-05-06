using AuthContext.Presentation.Common;

namespace AuthContext.Presentation.Auth.Endpoints.OnboardUser;

public record OnboardUserResponse(
    string? ErrorDescription,
    string? ErrorCode
) : IApiResponse;
