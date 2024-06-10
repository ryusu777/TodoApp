using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Common.ValueObjects;

namespace ProjectManagement.Application.Assignment.Commands.ReopenAssignment;

public class ReopenAssignmentCommandHandler : ICommandHandler<ReopenAssignmentCommand>
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReopenAssignmentCommandHandler(IUnitOfWork unitOfWork, IAssignmentRepository assignmentRepository)
    {
        _unitOfWork = unitOfWork;
        _assignmentRepository = assignmentRepository;
    }

    public async Task<Result> Handle(ReopenAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignmentResult = await _assignmentRepository.GetAssignmentById(AssignmentId.Create(request.AssignmentId), cancellationToken);

        if (assignmentResult.IsFailure || assignmentResult.Value is null)
        {
            return assignmentResult;
        }

        var result = assignmentResult.Value.Renew(UserId.Create(request.UserId));

        if (result.IsFailure)
        {
            return result;
        }

        _unitOfWork.AddEventsQueue(assignmentResult.Value.DomainEvents);

        return await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

