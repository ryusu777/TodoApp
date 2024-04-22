using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Common.ValueObjects;

namespace ProjectManagement.Application.Assignment.Commands.Assigning;

public class AssigningCommandHandler : ICommandHandler<AssigningCommand>
{
	private readonly IUnitOfWork _unitOfWork;
    private readonly IAssignmentRepository _assignmentRepository;
    public AssigningCommandHandler(
        IUnitOfWork unitOfWork,
        IAssignmentRepository assignmentRepository
    ) {
        _unitOfWork = unitOfWork;
        _assignmentRepository = assignmentRepository;
    }
    public async Task<Result> Handle(AssigningCommand request, CancellationToken cancellationToken)
	{
        var result = await _assignmentRepository
            .GetAssignmentById(AssignmentId.Create(request.AssignmentId), cancellationToken);

        if (result.Value is null)
        {
            return result;
        }

        result.Value.Assign(UserId.Create(request.AssigneeUsername));

        return await _unitOfWork.SaveChangesAsync(result.Value.DomainEvents, cancellationToken);
	}
}
