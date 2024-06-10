using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Common.ValueObjects;

namespace ProjectManagement.Application.Assignment.Commands.ApproveAssignmentReview;

public class ApproveAssignmentReviewCommandHandler : ICommandHandler<ApproveAssignmentReviewCommand>
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ApproveAssignmentReviewCommandHandler(IUnitOfWork unitOfWork, IAssignmentRepository assignmentRepository)
    {
        _unitOfWork = unitOfWork;
        _assignmentRepository = assignmentRepository;
    }

    public async Task<Result> Handle(ApproveAssignmentReviewCommand request, CancellationToken cancellationToken)
    {
        var assignmentResult = await _assignmentRepository.GetAssignmentById(AssignmentId.Create(request.AssignmentId), cancellationToken);
        if (assignmentResult.IsFailure || assignmentResult.Value is null)
        {
            return assignmentResult;
        }

        var result = assignmentResult.Value.ApproveCompletion(UserId.Create(request.UserId));
        if (result.IsFailure)
            return result;

        _unitOfWork.AddEventsQueue(assignmentResult.Value.DomainEvents);

        return await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

