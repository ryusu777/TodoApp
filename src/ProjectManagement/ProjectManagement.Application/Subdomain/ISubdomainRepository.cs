using Library.Models;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Subdomain;

public interface ISubdomainRepository
{
    public Task<Result<Domain.Subdomain.Subdomain>> GetSubdomainById(SubdomainId id);
}
