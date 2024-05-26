using Library.Models;
using ProjectManagement.Application.Assignment.Dtos;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Assignment;

public interface IAssignmentRepository
{
	public Task<Result<Domain.Assignment.Assignment>> GetAssignmentById(AssignmentId id, CancellationToken ct);
	public Task<Result<Dtos.Assignment>> GetAssignmentAsDtoById(AssignmentId id, CancellationToken ct);
	public Task<Result<IEnumerable<Dtos.Assignment>>> GetAssignments(ProjectId id, CancellationToken ct);
	public Task<Result<IEnumerable<Dtos.Assignment>>> GetAssignmentsBySubdomain(
        ProjectId projectId, 
        SubdomainId? subdomainId, 
        CancellationToken ct);
	public Task<Result<int>> GetOpenedAssignmentsCountBySubdomain(
        SubdomainId? subdomainId,
        CancellationToken ct);
    public Task<Result<IEnumerable<AssignmentCount>>> GetOpenedAssignmentCountPerSubdomain(IEnumerable<SubdomainId> subdomainIds, CancellationToken ct);

    public Task<Result<int>> GetNewAssignmentCount(ProjectId projectId, CancellationToken ct);
}
