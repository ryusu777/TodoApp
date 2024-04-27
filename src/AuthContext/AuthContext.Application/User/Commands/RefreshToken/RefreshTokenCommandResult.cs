using AuthContext.Application.Identity.Model;

namespace AuthContext.Application.User.Commands.RefreshingToken;

public record RefreshingTokenCommandResult : AuthenticationResult
{
    public RefreshingTokenCommandResult(
        string access_token, 
        long expires_in, 
        string token_type, 
        string refresh_token) : base(access_token, expires_in, token_type, refresh_token)
    {
    }
}
