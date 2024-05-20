using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Assignment.ValueObjects;

namespace ProjectManagement.Application.Assignment.Commands.CompleteAssignmentFromHook;

public class CompleteAssignmentFromHookCommandHandler : ICommandHandler<CompleteAssignmentFromHookCommand>
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteAssignmentFromHookCommandHandler(IAssignmentRepository assignmentRepository, IUnitOfWork unitOfWork)
    {
        _assignmentRepository = assignmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CompleteAssignmentFromHookCommand request, CancellationToken cancellationToken)
    {
        var assignmentResult = await _assignmentRepository.GetAssignmentById(AssignmentId.Create(request.Message.AssignmentId), cancellationToken);

        if (assignmentResult.IsFailure || assignmentResult.Value is null)
            return assignmentResult;

        assignmentResult.Value.CompleteFromHook();
        
        return await _unitOfWork.SaveChangesAsync(assignmentResult.Value.DomainEvents, cancellationToken);
    }
}

