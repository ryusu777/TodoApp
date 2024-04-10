using Library.Models;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Assignment;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Infrastructure.Persistence.Data;

namespace ProjectManagement.Infrastructure.Assignment.Repositories;

public class AssignmentRepository : IAssignmentRepository
{
	private readonly AppDbContext _dbContext;

	public AssignmentRepository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<Result<Domain.Assignment.Assignment>> GetAssignmentById(AssignmentId id)
	{
		var result = await _dbContext
			.Assignments
			.Include(e => e.Assignees)
			.FirstOrDefaultAsync(e => e.Id == id);

		if (result is null)
			return Result.Failure<Domain.Assignment.Assignment>(AssignmentInfrastructureError.AssignmentNotFound);

		return Result.Success(result);
	}
}
