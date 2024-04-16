using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Subdomain.Commands.UpdateSubdomain;

public class UpdateSubdomainCommandHandler : ICommandHandler<UpdateSubdomainCommand>
{
    private readonly ISubdomainRepository _subdomainRepo;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSubdomainCommandHandler(IUnitOfWork unitOfWork, ISubdomainRepository subdomainRepo)
    {
        _unitOfWork = unitOfWork;
        _subdomainRepo = subdomainRepo;
    }

    public async Task<Result> Handle(UpdateSubdomainCommand request, CancellationToken cancellationToken)
    {
        var result = await _subdomainRepo
            .GetSubdomainById(SubdomainId.Create(request.SubdomainId), cancellationToken);

        if (result.Value is null)
            return result;

        result.Value.Update(request.Title, request.Description);

        return await _unitOfWork
            .SaveChangesAsync(result.Value.DomainEvents, cancellationToken);
    }
}

