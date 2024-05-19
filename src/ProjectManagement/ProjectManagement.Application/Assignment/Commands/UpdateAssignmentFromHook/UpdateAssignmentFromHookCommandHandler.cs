using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Common.ValueObjects;

namespace ProjectManagement.Application.Assignment.Commands.UpdateAssignmentFromHook;

public class UpdateAssignmentFromHookCommandHandler : ICommandHandler<UpdateAssignmentFromHookCommand>
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAssignmentFromHookCommandHandler(IAssignmentRepository assignmentRepository, IUnitOfWork unitOfWork)
    {
        _assignmentRepository = assignmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateAssignmentFromHookCommand request, CancellationToken cancellationToken)
    {
        var result = await _assignmentRepository
            .GetAssignmentById(AssignmentId.Create(request.Message.AssignmentId), cancellationToken);

        if (result.Value is null)
            return result;

        result.Value
            .UpdateFromHook(
                request.Message.Title, 
                request.Message.Body,
                request.Message.Assignees
                    .Select(e => UserId.Create(e))
                    .ToList());

        return await _unitOfWork
            .SaveChangesAsync(result.Value.DomainEvents, cancellationToken);
    }
}

