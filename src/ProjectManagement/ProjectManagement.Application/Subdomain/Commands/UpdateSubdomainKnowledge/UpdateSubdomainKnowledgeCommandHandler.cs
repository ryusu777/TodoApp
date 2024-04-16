using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Subdomain.Commands.UpdateSubdomainKnowledge;

public class UpdateSubdomainKnowledgeCommandHandler
    : ICommandHandler<UpdateSubdomainKnowledgeCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISubdomainRepository _subdomainRepository;

    public UpdateSubdomainKnowledgeCommandHandler(ISubdomainRepository subdomainRepository, IUnitOfWork unitOfWork)
    {
        _subdomainRepository = subdomainRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateSubdomainKnowledgeCommand request, CancellationToken cancellationToken)
    {
        var subdomain = await _subdomainRepository
            .GetSubdomainById(SubdomainId.Create(request.SubdomainId), cancellationToken);

        if (subdomain.Value is null)
            return subdomain;

        subdomain.Value
            .UpdateKnowledge(
                SubdomainKnowledgeId
                    .Create(request.SubdomainKnowledgeId),
                request.Title, 
                request.Content);

        return await _unitOfWork
            .SaveChangesAsync(subdomain.Value.DomainEvents, cancellationToken);
    }
}
