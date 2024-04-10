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

	public async Task<Result<Domain.Subdomain.Subdomain>> GetSubdomainById(SubdomainId id)
	{
		var result = await _dbContext
			.Subdomains
			.Include(e => e.Knowledges)
			.FirstOrDefaultAsync(e => e.Id == id);

		if (result is null)
			return Result.Failure<Domain.Subdomain.Subdomain>(SubdomainInfrastructureError.SubdomainNotFound);

		return Result.Success(result);
	}
}
