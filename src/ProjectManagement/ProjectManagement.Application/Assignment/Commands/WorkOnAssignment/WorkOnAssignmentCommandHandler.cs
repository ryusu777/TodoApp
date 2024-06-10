using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Assignment.ValueObjects;

namespace ProjectManagement.Application.Assignment.Commands.WorkOnAssignment;

public class WorkOnAssignmentCommandHandler : ICommandHandler<WorkOnAssignmentCommand>
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public WorkOnAssignmentCommandHandler(IAssignmentRepository assignmentRepository, IUnitOfWork unitOfWork)
    {
        _assignmentRepository = assignmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(WorkOnAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignmentResult = await _assignmentRepository.GetAssignmentById(AssignmentId.Create(request.AssignmentId), cancellationToken);
        if (assignmentResult.IsFailure || assignmentResult.Value is null)
        {
            return assignmentResult;
        }

        var result = assignmentResult.Value.WorkOn();
        if (result.IsFailure)
            return result;

        _unitOfWork.AddEventsQueue(assignmentResult.Value.DomainEvents);

        return await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

