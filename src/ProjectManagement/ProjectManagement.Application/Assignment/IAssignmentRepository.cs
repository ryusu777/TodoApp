using Library.Models;
using ProjectManagement.Domain.Assignment.ValueObjects;

namespace ProjectManagement.Application.Assignment;

public interface IAssignmentRepository
{
	public Task<Result<Domain.Assignment.Assignment>> GetAssignmentById(AssignmentId id);
}
