using AuthContext.Application.User;
using AuthContext.Domain.User;
using AuthContext.Domain.User.ValueObjects;
using AuthContext.Infrastructure.Persistence.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthContext.Infrastructure.User;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Domain.User.User>> GetUserByUsernameAsync(string username, CancellationToken ct)
    {
        var result = await _dbContext.Users.FirstOrDefaultAsync(e => e.UserName == username, ct);
        if (result is null)
            return Result.Failure<Domain.User.User>(UserDomainError.UserNotFound);

        return Result.Success(
            Domain.User.User.Create(
                UserId.Create(result.UserName!),
                Domain.User.ValueObjects.Email.Create(result.Email!),
                result.RefreshToken
            )
        );
    }
}
