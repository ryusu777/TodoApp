using Library.Models;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Subdomain;

public interface ISubdomainRepository
{
    public Task<Result<Domain.Subdomain.Subdomain>> GetSubdomainById(SubdomainId id, CancellationToken ct);
    public Task<Result<IEnumerable<Domain.Subdomain.Subdomain>>> GetSubdomains(ProjectId id, CancellationToken ct);
}
