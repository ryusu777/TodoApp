using Library.Models;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Project;
using ProjectManagement.Application.Project.Queries.GetProjectPages;
using ProjectManagement.Domain.Project.Enums;
using ProjectManagement.Domain.Project.ValueObjects;
using ProjectManagement.Infrastructure.Persistence.Data;

namespace ProjectManagement.Infrastructure.Project.Repositories;

public class ProjectRepository : IProjectRepository
{
	private readonly AppDbContext _dbContext;

	public ProjectRepository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<Result<Domain.Project.Project>> GetProjectById(ProjectId id, CancellationToken ct)
	{
		var result = await _dbContext
			.Projects
			.Include(e => e.ProjectPhases)
			.FirstOrDefaultAsync(e => e.Id == id, ct);

		if (result is null)
			return Result.Failure<Domain.Project.Project>(ProjectInfrastructureError.ProjectNotFound);

		return Result.Success(result);
	}

    public async Task<Result<IEnumerable<GetProjectPagesResult>>> GetProjectPages(CancellationToken ct)
    {
        var result = await _dbContext
            .Projects
            .Where(e => 
                e.Status == ProjectStatus.Active || 
                e.Status == ProjectStatus.Planning)
            .Select(e => new GetProjectPagesResult(e.Id.Value, e.Name))
            .ToListAsync(ct);

        return Result.Success(result.AsEnumerable());
    }
}
