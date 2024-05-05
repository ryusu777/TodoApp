using AuthContext.Application.Abstractions.Data;
using AuthContext.Application.Abstractions.Messaging;
using AuthContext.Application.Identity;
using AuthContext.Domain.User;
using AuthContext.Domain.User.Events;
using AuthContext.Domain.User.ValueObjects;
using Library.Models;

namespace AuthContext.Application.User.Commands.AuthorizeGitea;

public class AuthorizeGiteaCommandHandler : ICommandHandler<AuthorizeGiteaCommand, AuthorizeGiteaResult>
{
    private readonly IAuthenticationService _authService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AuthorizeGiteaCommandHandler(IAuthenticationService authService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _authService = authService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AuthorizeGiteaResult>> Handle(AuthorizeGiteaCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService
            .GrantGiteaAccessToken(request.AuthorizationCode, 
                cancellationToken);

        if (result.IsFailure || result.Value is null)
            return Result.Failure<AuthorizeGiteaResult>(result.Error);

        var foundUser = await _userRepository
            .GetUserByUsernameAsync(
                result.Value.Username, 
                cancellationToken);

        UserId username = UserId.Create(result.Value.Username);
        Domain.User.ValueObjects.Email userEmail = Domain.User.ValueObjects.Email.Create(result.Value.Email);

        if (foundUser.Error == UserDomainError.UserNotFound)
        {
            return await PersistUser(username, userEmail, cancellationToken);
        }

        var authResult = await _authService.SignInAsync(username.Value, cancellationToken);
        
        if (authResult.IsFailure || authResult.Value is null)
        {
            return Result.Failure<AuthorizeGiteaResult>(authResult.Error);
        }

        return Result.Success(new AuthorizeGiteaResult(
            authResult.Value,
            null
        ));
    }

    private async Task<Result<AuthorizeGiteaResult>> PersistUser(
        UserId username, Domain.User.ValueObjects.Email userEmail, CancellationToken cancellationToken)
    {
        var registerResult = await _authService.RegisterUserAsync(
            username,
            userEmail,
            cancellationToken
        );

        if (registerResult.IsFailure)
            return Result
                .Failure<AuthorizeGiteaResult>(registerResult.Error);

        _unitOfWork.AddEventQueue(new UserCreated(
            Domain.User.User.Create(
                username,
                userEmail
            )));

        var persistResult = await _unitOfWork
            .SaveChangesAsync(cancellationToken);

        if (persistResult.IsFailure)
            return Result
                .Failure<AuthorizeGiteaResult>(persistResult.Error);

        var changePasswordToken = await _authService
            .RequestChangePasswordAsync(username.Value, cancellationToken);

        if (changePasswordToken.IsFailure || changePasswordToken.Value is null)
            return Result
                .Failure<AuthorizeGiteaResult>(persistResult.Error);

        return Result
            .Success(new AuthorizeGiteaResult(
                null,
                new AuthorizeGiteaOnboardResult(
                    changePasswordToken.Value,
                    username.Value,
                    userEmail.Value
                )
            ));
    }
}
