using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Assignment.ValueObjects;

namespace ProjectManagement.Application.Assignment.Commands.UpdateAssignments;

public class UpdateAssignmentCommandHandler : ICommandHandler<UpdateAssignmentCommand>
{
	private readonly IUnitOfWork _unitOfWork;
    private readonly IAssignmentRepository _assignmentRepository;

    public UpdateAssignmentCommandHandler(IAssignmentRepository assignmentRepository, IUnitOfWork unitOfWork)
    {
        _assignmentRepository = assignmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
    {
        var result = await _assignmentRepository
            .GetAssignmentById(AssignmentId.Create(request.AssignmentId));

        if (result.Value is null)
            return result;

        result.Value.Update(request.Title, request.Description);

        return await _unitOfWork
            .SaveChangesAsync(result.Value.DomainEvents, cancellationToken);
    }
}
