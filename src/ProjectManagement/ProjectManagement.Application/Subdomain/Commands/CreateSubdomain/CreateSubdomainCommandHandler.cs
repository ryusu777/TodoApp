using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Subdomain.Events;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Subdomain.Commands.CreateSubdomain;

public class CreateSubdomainCommandHandler : ICommandHandler<CreateSubdomainCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubdomainCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateSubdomainCommand request, CancellationToken cancellationToken)
    {
        var result = Domain.Subdomain.Subdomain.Create(
            request.Description,
            request.Title,
            ProjectId.Create(request.ProjectId)
        );

        return await _unitOfWork.SaveChangesAsync(new SubdomainCreated(result), cancellationToken);
    }
}

