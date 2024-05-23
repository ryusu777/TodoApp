using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Assignment.Commands.ChangeAssignmentStatus;
using ProjectManagement.Domain.Assignment.Enums;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Common.ValueObjects;

namespace ProjectManagement.Application.Assignment.Commands;

public class ChangeAssignmentStatusCommandHandler : ICommandHandler<ChangeAssignmentStatusCommand>
{
	private readonly IUnitOfWork _unitOfWork;

    private readonly IAssignmentRepository _assignmentRepository;

    public ChangeAssignmentStatusCommandHandler(IUnitOfWork unitOfWork, IAssignmentRepository assignmentRepository)
    {
        _unitOfWork = unitOfWork;
        _assignmentRepository = assignmentRepository;
    }

    public async Task<Result> Handle(ChangeAssignmentStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await _assignmentRepository
            .GetAssignmentById(AssignmentId.Create(request.AssignmentId), cancellationToken);

        if (result.Value is null)
            return result;

        Result<AssignmentStatus> statusResult = AssignmentStatus.Create(request.AssignmentStatus);

        if (statusResult.IsFailure || statusResult.Value is null)
            return statusResult;
        
        Result changeStatusResult;

        switch (statusResult.Value.Value)
        {
            case AssignmentStatusEnum.New:
                changeStatusResult = result.Value.Renew(UserId.Create(request.UserId));
                break;

            case AssignmentStatusEnum.Completed:
                changeStatusResult = result.Value.Complete(UserId.Create(request.UserId));
                break;

            case AssignmentStatusEnum.OnProgress:
                changeStatusResult = result.Value.WorkOn();
                break;

            case AssignmentStatusEnum.WaitingReview:
                changeStatusResult = result.Value.RequestReview();
                break;

            default:
                changeStatusResult = Result.Failure(Error.None);
                break;
        }

        if (changeStatusResult.IsFailure)
            return changeStatusResult;

        return await _unitOfWork
            .SaveChangesAsync(result.Value.DomainEvents, cancellationToken);
    }
}
