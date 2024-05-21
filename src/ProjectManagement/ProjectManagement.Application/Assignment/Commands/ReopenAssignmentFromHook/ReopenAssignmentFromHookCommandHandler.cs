using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Assignment.ValueObjects;

namespace ProjectManagement.Application.Assignment.Commands.ReopenAssignmentFromHook;

public class ReopenAssignmentFromHookCommandHandler : ICommandHandler<ReopenAssignmentFromHookCommand>
{
    private readonly IAssignmentRepository _assignmentRepo;
    private readonly IUnitOfWork _unitOfWork;

    public ReopenAssignmentFromHookCommandHandler(IAssignmentRepository assignmentRepo, IUnitOfWork unitOfWork)
    {
        _assignmentRepo = assignmentRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ReopenAssignmentFromHookCommand request, CancellationToken cancellationToken)
    {
        var assignmentResult = await _assignmentRepo.GetAssignmentById(AssignmentId.Create(request.AssignmentId), cancellationToken);

        if (assignmentResult.IsFailure || assignmentResult.Value is null)
            return assignmentResult;

        assignmentResult.Value.RenewFromHook();
        return await _unitOfWork.SaveChangesAsync(assignmentResult.Value.DomainEvents, cancellationToken);
    }
}
