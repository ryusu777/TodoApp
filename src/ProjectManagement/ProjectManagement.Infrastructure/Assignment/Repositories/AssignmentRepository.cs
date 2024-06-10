using Library.Models;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Assignment;
using ProjectManagement.Application.Assignment.Dtos;
using ProjectManagement.Domain.Assignment.Enums;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;
using ProjectManagement.Infrastructure.Persistence.Data;

namespace ProjectManagement.Infrastructure.Assignment.Repositories;

public class AssignmentRepository : IAssignmentRepository
{
	private readonly AppDbContext _dbContext;

	public AssignmentRepository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

    private static string? GetReviewer(UserId? id) => id is null ? null : id.Value;

	public async Task<Result<Domain.Assignment.Assignment>> GetAssignmentById(AssignmentId id, CancellationToken ct)
	{
		var result = await _dbContext
			.Assignments
			.Include(e => e.Assignees)
			.FirstOrDefaultAsync(e => e.Id == id, ct);

		if (result is null)
			return Result.Failure<Domain.Assignment.Assignment>(AssignmentInfrastructureError.AssignmentNotFound);

		return Result.Success(result);
	}

    public async Task<Result<IEnumerable<Application.Assignment.Dtos.Assignment>>> GetAssignments(ProjectId id, CancellationToken ct)
    {
        var result = await _dbContext
            .Assignments
            .Where(e => e.ProjectId == id)
            .AsNoTracking()
            .ToListAsync();

		if (result is null)
			return Result.Failure<IEnumerable<Application.Assignment.Dtos.Assignment>>(AssignmentInfrastructureError.AssignmentNotFound);

		return Result.Success(result
            .Select(assignment => new Application.Assignment.Dtos.Assignment(
                assignment.Id.Value,
                assignment.Title,
                assignment.Description,
                assignment.ProjectId.Value,
                assignment.Status.Value.ToString(),
                assignment.SubdomainId?.Value,
                assignment.PhaseId?.Value,
                assignment.Assignees.Select(e => e.Value).ToList(), 
                GetReviewer(assignment.Reviewer),
                assignment.Deadline,
                Application.Assignment.Dtos.Review.FromDomain(assignment.Reviews.OrderByDescending(e => e.CreatedAt).FirstOrDefault())
            )));
    }

    public async Task<Result<Application.Assignment.Dtos.Assignment>> GetAssignmentAsDtoById(AssignmentId id, CancellationToken ct)
    {
        var assignment = await _dbContext
            .Assignments
            .FirstOrDefaultAsync(e => e.Id == id);

		if (assignment is null)
			return Result.Failure<Application.Assignment.Dtos.Assignment>(AssignmentInfrastructureError.AssignmentNotFound);

		return Result.Success(new Application.Assignment.Dtos.Assignment(
            assignment.Id.Value,
            assignment.Title,
            assignment.Description,
            assignment.ProjectId.Value,
            assignment.Status.Value.ToString(),
            assignment.SubdomainId?.Value,
            assignment.PhaseId?.Value,
            assignment.Assignees.Select(e => e.Value).ToList(),
            GetReviewer(assignment.Reviewer),
            assignment.Deadline,
            Application.Assignment.Dtos.Review.FromDomain(assignment.Reviews.OrderByDescending(e => e.CreatedAt).FirstOrDefault())));
    }

    public async Task<Result<IEnumerable<Application.Assignment.Dtos.Assignment>>> GetAssignmentsBySubdomain(ProjectId projectId, SubdomainId? subdomainId, CancellationToken ct)
    {
        var result = await _dbContext
            .Assignments
            .Where(e => e.ProjectId == projectId && e.SubdomainId == subdomainId)
            .ToListAsync();

		if (result is null)
			return Result.Failure<IEnumerable<Application.Assignment.Dtos.Assignment>>(AssignmentInfrastructureError.AssignmentNotFound);

		return Result.Success(result
            .Select(assignment => new Application.Assignment.Dtos.Assignment(
                assignment.Id.Value,
                assignment.Title,
                assignment.Description,
                assignment.ProjectId.Value,
                assignment.Status.Value.ToString(),
                assignment.SubdomainId?.Value,
                assignment.PhaseId?.Value,
                assignment.Assignees.Select(e => e.Value).ToList(), 
                GetReviewer(assignment.Reviewer),
                assignment.Deadline,
                Application.Assignment.Dtos.Review.FromDomain(assignment.Reviews.OrderByDescending(e => e.CreatedAt).FirstOrDefault())
            )));
    }

    public async Task<Result<IEnumerable<AssignmentCount>>> GetOpenedAssignmentCountPerSubdomain(IEnumerable<SubdomainId> subdomainIds, CancellationToken ct)
    {
        var result = await _dbContext
            .Assignments
            .Where(e => subdomainIds.Contains(e.SubdomainId) && e.Status != new AssignmentStatus(AssignmentStatusEnum.Completed))
            .GroupBy(e => e.SubdomainId)
            .Select(e => new AssignmentCount(e.Key!.Value, e.Count()))
            .ToListAsync(ct);

        return Result.Success(result.AsEnumerable());
    }

    public async Task<Result<int>> GetOpenedAssignmentsCountBySubdomain(SubdomainId? subdomainId, CancellationToken ct)
    {
        var result = await _dbContext
            .Assignments
            .CountAsync(e => e.SubdomainId == subdomainId && e.Status != new AssignmentStatus(AssignmentStatusEnum.Completed), ct);

        return Result.Success(result);
    }

    public async Task<Result<int>> GetNewAssignmentCount(ProjectId projectId, CancellationToken ct)
    {
        var result = await _dbContext
            .Assignments
            .CountAsync(e => e.ProjectId == projectId && e.SubdomainId == null);

        return Result.Success(result);
    }
}
