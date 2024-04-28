using IntegrationContext.Domain.Auth;
using IntegrationContext.Domain.Auth.ValueObjects;
using Library.Models;

namespace IntegrationContext.Application.Auth;

public interface IUserRepository
{
    public Task<Result<GiteaUser>> GetGiteaUserByUsername(UserId username, CancellationToken ct);
}
