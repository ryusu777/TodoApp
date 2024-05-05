using Library.Models;

namespace AuthContext.Application.User;

public interface IUserRepository
{
    public Task<Result<Domain.User.User>> GetUserByUsernameAsync(string username, CancellationToken ct);
}
