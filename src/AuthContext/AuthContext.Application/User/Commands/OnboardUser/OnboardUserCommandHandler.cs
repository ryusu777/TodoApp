using AuthContext.Application.Abstractions.Data;
using AuthContext.Application.Abstractions.Messaging;
using AuthContext.Application.User.Events;
using AuthContext.Domain.User.ValueObjects;
using Library.Models;

namespace AuthContext.Application.User.Commands.OnboardUser;

public class OnboardUserCommandHandler : ICommandHandler<OnboardUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OnboardUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(OnboardUserCommand request, CancellationToken cancellationToken)
    {
        var findUserResult = await _userRepository
            .GetUserByUsernameAsync(request.Username, cancellationToken);

        if (findUserResult is not null)
            return UserApplicationError.UserAlreadyOnboarded;

        var createdUser = Domain.User.User
            .Create(
                UserId.Create(request.Username),
                Domain.User.ValueObjects.Email.Create(request.Email),
                GiteaUserId.Create(request.GiteaUserId)
            );

        _unitOfWork.AddEventQueue(new UserCreated(createdUser));

        return await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

