using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Assignment.ValueObjects;

namespace ProjectManagement.Application.Assignment.Commands.RequestAssignmentReview;

public class RequestAssignmentReviewCommandHandler : ICommandHandler<RequestAssignmentReviewCommand>
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RequestAssignmentReviewCommandHandler(IUnitOfWork unitOfWork, IAssignmentRepository assignmentRepository)
    {
        _unitOfWork = unitOfWork;
        _assignmentRepository = assignmentRepository;
    }

    public async Task<Result> Handle(RequestAssignmentReviewCommand request, CancellationToken cancellationToken)
    {
        var assignmentResult = await _assignmentRepository.GetAssignmentById(AssignmentId.Create(request.AssignmentId), cancellationToken);
        if (assignmentResult.IsFailure || assignmentResult.Value is null)
        {
            return assignmentResult;
        }

        var result = assignmentResult.Value.RequestReview(request.Description);
        if (result.IsFailure)
            return result;
        _unitOfWork.AddEventsQueue(assignmentResult.Value.DomainEvents);

        return await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

