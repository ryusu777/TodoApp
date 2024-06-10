using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Assignment.ValueObjects;

namespace ProjectManagement.Application.Assignment.Commands.RejectAssignmentReview;

public class RejectAssignmentReviewCommandHandler : ICommandHandler<RejectAssignmentReviewCommand>
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RejectAssignmentReviewCommandHandler(IUnitOfWork unitOfWork, IAssignmentRepository assignmentRepository)
    {
        _unitOfWork = unitOfWork;
        _assignmentRepository = assignmentRepository;
    }

    public async Task<Result> Handle(RejectAssignmentReviewCommand request, CancellationToken cancellationToken)
    {
        var assignmentResult = await _assignmentRepository.GetAssignmentById(AssignmentId.Create(request.AssignmentId), cancellationToken);
        if (assignmentResult.IsFailure || assignmentResult.Value is null)
        {
            return assignmentResult;
        }

        var result = assignmentResult.Value.RejectCompletion(request.RejectionNotes);
        if (result.IsFailure)
            return result;

        _unitOfWork.AddEventsQueue(assignmentResult.Value.DomainEvents);

        return await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

