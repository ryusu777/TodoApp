using AuthContext.Application.Identity.Model;

namespace AuthContext.Application.User.Commands.SignIn;

public record SignInCommandResult : AuthenticationResult
{
    public bool Registered { get; private set; }
    public SignInCommandResult(
        string access_token,
        long expires_in,
        string token_type,
        string refresh_token,
        bool registered) : base(access_token, expires_in, token_type, refresh_token)
    {
        Registered = registered;
    }
}

