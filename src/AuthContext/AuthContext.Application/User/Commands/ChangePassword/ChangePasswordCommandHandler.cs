using AuthContext.Application.Abstractions.Messaging;
using Library.Models;

namespace AuthContext.Application.User.Commands.ChangePassword;

public class ChangePasswordCommandHandler
    : ICommandHandler<ChangePasswordCommand>
{
    public Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
