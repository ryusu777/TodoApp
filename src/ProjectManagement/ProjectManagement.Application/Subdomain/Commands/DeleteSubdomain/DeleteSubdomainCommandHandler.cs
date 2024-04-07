using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Subdomain.Events;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Subdomain.Commands.DeleteSubdomain;

public class DeleteSubdomainCommandHandler : ICommandHandler<DeleteSubdomainCommand>
{
    private readonly ISubdomainRepository _subdomainRepo;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSubdomainCommandHandler(IUnitOfWork unitOfWork, ISubdomainRepository subdomainRepo)
    {
        _unitOfWork = unitOfWork;
        _subdomainRepo = subdomainRepo;
    }

    public async Task<Result> Handle(DeleteSubdomainCommand request, CancellationToken cancellationToken)
    {
        var result = await _subdomainRepo.GetSubdomainById(SubdomainId.Create(request.SubdomainId));
        
        if (result.Value is null)
            return result;

        return await _unitOfWork
            .SaveChangesAsync(new SubdomainDeleted(result.Value.Id), cancellationToken);
    }
}

