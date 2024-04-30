using AuthContext.Application.Abstractions.Messaging;
using AuthContext.Application.Identity;
using Library.Models;

namespace AuthContext.Application.User.Commands.SignInWithGitea;

public class SignInWithGiteaCommandHandler : ICommandHandler<SignInWithGiteaCommand, Uri>
{
    private IAuthenticationService _authService;

    public SignInWithGiteaCommandHandler(IAuthenticationService authService)
    {
        _authService = authService;
    }

    public async Task<Result<Uri>> Handle(SignInWithGiteaCommand request, CancellationToken cancellationToken)
    {
        Guid state = Guid.NewGuid();
        var result = await _authService.GetGiteaAuthProviderUrl(state, cancellationToken);

        if (result.IsFailure || result.Value is null)
            return Result.Failure<Uri>(result.Error);

        return result;
    }
}
