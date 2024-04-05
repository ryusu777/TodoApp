using Library.Models;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project;

public interface IProjectRepository
{
	public Task<Result<Domain.Project.Project>> GetProjectById(ProjectId id);
}
