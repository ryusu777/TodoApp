using AuthContext.Application.Abstractions.Data;
using AuthContext.Application.Abstractions.Messaging;
using AuthContext.Domain.User.Events;
using AuthContext.Domain.User;
using AuthContext.Domain.User.ValueObjects;
using Library.Models;
using AuthContext.Application.Identity;

namespace AuthContext.Application.User.Commands.OnboardUser;

public class OnboardUserCommandHandler : ICommandHandler<OnboardUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthenticationService _authService;

    public OnboardUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IAuthenticationService authService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public async Task<Result> Handle(OnboardUserCommand request, CancellationToken cancellationToken)
    {
        var userOnboarded = await _authService
            .IsUserOnboarded(request.Username, cancellationToken);

        if (userOnboarded)
            return UserDomainError.UserAlreadyOnboarded;

        var createdUser = Domain.User.User
            .Create(
                UserId.Create(request.Username),
                Domain.User.ValueObjects.Email.Create(request.Email)
            );

        _unitOfWork.AddEventQueue(new UserCreated(createdUser));

        var changePasswordResult = await _authService.ChangePasswordAsync(
            request.Username, request.Password, request.ChangePasswordCode, cancellationToken);

        if (changePasswordResult.IsFailure)
            return changePasswordResult.Error;

        return await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

