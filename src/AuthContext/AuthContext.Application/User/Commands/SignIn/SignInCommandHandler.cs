using AuthContext.Application.Abstractions.Data;
using AuthContext.Application.Abstractions.Messaging;
using AuthContext.Application.Identity;
using AuthContext.Domain.User.Events;
using AuthContext.Domain.User.ValueObjects;
using Library.Models;

namespace AuthContext.Application.User.Commands.SignIn;

public class SignInCommandHandler : ICommandHandler<SignInCommand, SignInCommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public SignInCommandHandler(IUserRepository userRepository, IAuthenticationService identityService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _identityService = identityService;
        _unitOfWork = unitOfWork;
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

        if (findUserResult.Value is not null)
        {
            RefreshToken refreshToken = RefreshToken.Create(signInResult.Value.refresh_token);

            findUserResult.Value
                .AddRefreshToken(
                    _identityService.GetJti(JwtToken.Create(tokenResult.access_token)),
                    refreshToken,
                    refreshToken.GetExpiryInUtc()
                );
            _unitOfWork.AddEventsQueue(findUserResult.Value.DomainEvents);
        }

        var persistResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (persistResult.IsFailure)
            return Result.Failure<SignInCommandResult>(persistResult.Error);

        return Result
            .Success<SignInCommandResult>(new SignInCommandResult(
                tokenResult.access_token,
                tokenResult.expires_in,
                tokenResult.token_type,
                tokenResult.refresh_token,
                findUserResult.Value is not null));
    }
}

