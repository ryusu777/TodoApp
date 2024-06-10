using Library.Models;
using ProjectManagement.Application.Project.Queries.GetProjectPages;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project;

public interface IProjectRepository
{
	public Task<Result<Domain.Project.Project>> GetProjectById(ProjectId id, CancellationToken ct);
    public Task<Result<IEnumerable<GetProjectPagesResult>>> GetProjectPages(CancellationToken ct);
    public Task<Result<List<Domain.Project.Project>>> GetActiveProjects(CancellationToken ct);
}
