using AuthContext.Application.User;
using Library.Models;

namespace AuthContext.Infrastructure.User;

public class UserRepository : IUserRepository
{
    public Task<Result<Domain.User.User>> GetUserByUsernameAsync(string username, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result> RegisterUserAsync(string username, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
