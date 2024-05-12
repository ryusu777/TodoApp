using IntegrationContext.Domain.Auth;
using IntegrationContext.Domain.Auth.ValueObjects;
using Library.Models;

namespace IntegrationContext.Application.Auth;

public interface IGiteaUserDomainService
{
	public Task<Result<GiteaUser>> GetOrRefreshJwt(UserId userId, CancellationToken ct);
}
