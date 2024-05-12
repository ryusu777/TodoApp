using AuthContext.Application.Abstractions.Data;
using AuthContext.Application.Abstractions.Messaging;
using AuthContext.Application.Identity;
using AuthContext.Domain.User.ValueObjects;
using Library.Models;

namespace AuthContext.Application.User.Commands.RefreshingToken;

public class RefreshingTokenCommandHandler : ICommandHandler<RefreshingTokenCommand, RefreshingTokenCommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshingTokenCommandHandler(IAuthenticationService identityService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _identityService = identityService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RefreshingTokenCommandResult>> Handle(RefreshingTokenCommand request, CancellationToken cancellationToken)
    {
        var username = _identityService
            .GetUsername(JwtToken.Create(request.JwtToken));

        var userResult = await _userRepository.GetUserByUsernameAsync(username, cancellationToken);

        if (userResult.IsFailure || userResult.Value is null) 
        {
            return Failure(userResult.Error);
        }

        var refreshTokenResult = await _identityService
            .RefreshTokenAsync(
                JwtToken.Create(request.JwtToken),
                Domain.User.ValueObjects.RefreshToken.Create(request.RefreshToken),
                cancellationToken);

        if (refreshTokenResult.IsFailure || refreshTokenResult.Value is null)
        {
            return Failure(refreshTokenResult.Error);
        }

        var tokenResult = refreshTokenResult.Value;

        var user = userResult.Value;

        var revocationResult = user.RevokeRefreshToken(_identityService
            .GetJti(RefreshToken.Create(request.RefreshToken)));

        if (revocationResult.IsFailure)
            return Failure(revocationResult.Error);

        var persistingResult = await _unitOfWork.SaveChangesAsync(user.DomainEvents, cancellationToken);

        if (persistingResult.IsFailure)
            return Failure(persistingResult.Error);

        return Result
            .Success<RefreshingTokenCommandResult>(new RefreshingTokenCommandResult(
                tokenResult.access_token,
                tokenResult.expires_in,
                tokenResult.token_type,
                tokenResult.refresh_token));
    }

    private Result<RefreshingTokenCommandResult> Failure(Error error)
    {
        return Result.Failure<RefreshingTokenCommandResult>(error);
    }
}

