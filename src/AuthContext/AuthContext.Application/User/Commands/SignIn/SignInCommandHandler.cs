using AuthContext.Application.Abstractions.Messaging;
using AuthContext.Application.Identity;
using Library.Models;

namespace AuthContext.Application.User.Commands.SignIn;

public class SignInCommandHandler : ICommandHandler<SignInCommand, SignInCommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _identityService;

    public SignInCommandHandler(IUserRepository userRepository, IAuthenticationService identityService)
    {
        _userRepository = userRepository;
        _identityService = identityService;
    }

    public async Task<Result<SignInCommandResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var signInResult = await _identityService
            .SignInAsync(request.Username, request.Password, cancellationToken);

        if (signInResult.IsFailure || signInResult.Value is null)
            return Result.Failure<SignInCommandResult>(signInResult.Error);

        var tokenResult = signInResult.Value;
        
        var findUserResult = await _userRepository
            .GetUserByUsernameAsync(request.Username, cancellationToken);
        
        return Result
            .Success<SignInCommandResult>(new SignInCommandResult(
                tokenResult.access_token,
                tokenResult.expires_in,
                tokenResult.token_type,
                tokenResult.refresh_token,
                findUserResult.Value is null));
    }
}

