using AuthContext.Application.User.Commands.SignIn;
using AuthContext.Presentation.Common;

namespace AuthContext.Presentation.Auth.Endpoints.SignIn;

public record SignInResponse(
    string? ErrorDescription,
    string? ErrorCode,
    SignInCommandResult? Data 
) : IApiResponse<SignInCommandResult>;

