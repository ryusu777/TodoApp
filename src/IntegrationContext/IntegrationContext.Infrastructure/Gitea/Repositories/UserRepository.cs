using IntegrationContext.Application.Auth;
using IntegrationContext.Domain.Auth;
using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Infrastructure.Persistence.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace IntegrationContext.Infrastructure.Gitea.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GiteaUser>> GetGiteaUserByGiteaUserId(GiteaUserId id, CancellationToken ct)
    {
        var result = await _dbContext
            .GiteaUsers
            .FirstOrDefaultAsync(e => e.Id == id);

        if (result is null)
            return Result
                .Failure<GiteaUser>(AuthDomainError.UserNotFound);

        return Result.Success(result);
    }

    public async Task<Result<GiteaUser>> GetGiteaUserByUsername(UserId username, CancellationToken ct)
    {
        var result = await _dbContext
            .GiteaUsers
            .FirstOrDefaultAsync(e => e.UserId == username);

        if (result is null)
            return Result
                .Failure<GiteaUser>(AuthDomainError.UserNotFound);

        return Result.Success(result);
    }
}

