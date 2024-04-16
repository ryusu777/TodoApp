using Library.Models;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Subdomain;
using ProjectManagement.Domain.Project.ValueObjects;
using ProjectManagement.Infrastructure.Persistence.Data;

namespace ProjectManagement.Infrastructure.Subdomain.Repositories;

public class SubdomainRepository : ISubdomainRepository
{
	private readonly AppDbContext _dbContext;

	public SubdomainRepository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<Result<Domain.Subdomain.Subdomain>> GetSubdomainById(SubdomainId id, CancellationToken ct)
	{
		var result = await _dbContext
			.Subdomains
			.Include(e => e.Knowledges)
			.FirstOrDefaultAsync(e => e.Id == id, ct);

		if (result is null)
			return Result.Failure<Domain.Subdomain.Subdomain>(SubdomainInfrastructureError.SubdomainNotFound);

		return Result.Success(result);
	}

    public async Task<Result<IEnumerable<Domain.Subdomain.Subdomain>>> GetSubdomains(ProjectId id, CancellationToken ct)
    {
        var result = await _dbContext
            .Subdomains
            .Where(e => e.ProjectId == id)
            .ToListAsync();

        return Result.Success(result.AsEnumerable());
    }
}
