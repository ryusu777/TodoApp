using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Assignment.Events;
using ProjectManagement.Domain.Assignment.ValueObjects;

namespace ProjectManagement.Application.Assignment.Commands.DeleteAssignment;

public class DeleteAssignmentCommandHandler : ICommandHandler<DeleteAssignmentCommand>
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAssignmentCommandHandler(
        IAssignmentRepository assignmentRepository, 
        IUnitOfWork unitOfWork)
    {
        _assignmentRepository = assignmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
    {
        var result = await _assignmentRepository
            .GetAssignmentById(AssignmentId.Create(request.AssignmentId));

        if (result.Value is null) 
        {
            return result;
        }

        return await _unitOfWork
            .SaveChangesAsync(new AssignmentDeleted(result.Value.Id), cancellationToken);
    }
}
