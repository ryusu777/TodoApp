using Library.Models;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Project;
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

	public async Task<Result<Domain.Project.Project>> GetProjectById(ProjectId id)
	{
		var result = await _dbContext
			.Projects
			.Include(e => e.ProjectPhases)
			.FirstOrDefaultAsync(e => e.Id == id);

		if (result is null)
			return Result.Failure<Domain.Project.Project>(ProjectInfrastructureError.ProjectNotFound);

		return Result.Success(result);
	}
}
