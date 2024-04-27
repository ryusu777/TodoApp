using AuthContext.Application.Abstractions.Messaging;
using AuthContext.Application.Email;
using AuthContext.Application.Email.Model;
using AuthContext.Application.Identity;
using Library.Models;

namespace AuthContext.Application.User.Commands.RequestChangePassword;

public class RequestChangePasswordCommandHandler : ICommandHandler<RequestChangePasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authService;
    private readonly IEmailService _emailService;

    public RequestChangePasswordCommandHandler(IUserRepository userRepository, IAuthenticationService authService, IEmailService emailService)
    {
        _userRepository = userRepository;
        _authService = authService;
        _emailService = emailService;
    }

    public async Task<Result> Handle(RequestChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.Username, cancellationToken);

        if (user.Value is null)
            return Result.Failure(UserApplicationError.UserNotFound);

        Result<string> resetPasswordTokenResult = await _authService
            .RequestChangePasswordAsync(user.Value.Id.Value, cancellationToken);

        if (resetPasswordTokenResult.IsFailure)
            return resetPasswordTokenResult.Error;

        string resetLink = $"https://localhost:3000/reset-password/{resetPasswordTokenResult.Value}";

        return await _emailService
            .SendEmailAsync(new EmailContent(
                user.Value.Email,
                "Reset password for TodoApp",
                "<h1>Todo App Password Reset</h1> <br/>" + 
                "<p>If you did not request this, there might be someone accessing your account</p> <br />" +
                $"<p>Click this <a href=\"{resetLink}\">link</a> to reset your password</p>"
            ));
    }
}

