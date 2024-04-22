using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Common.ValueObjects;

namespace ProjectManagement.Application.Assignment.Commands;

public class RemoveAssigneeCommandHandler : ICommandHandler<RemoveAssigneeCommand>
{
    private readonly IAssignmentRepository _assignmentRepo;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveAssigneeCommandHandler(IAssignmentRepository assignmentRepo, IUnitOfWork unitOfWork)
    {
        _assignmentRepo = assignmentRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveAssigneeCommand request, CancellationToken cancellationToken)
    {
        var result = await _assignmentRepo.GetAssignmentById(
            AssignmentId.Create(request.AssignmentId), cancellationToken);

        if (result.Value is null)
        {
            return result;
        }

        result.Value.RemoveAssignee(UserId.Create(request.AssigneeUsername));
        
        return await _unitOfWork.SaveChangesAsync(
            result.Value.DomainEvents, cancellationToken);
    }
}
