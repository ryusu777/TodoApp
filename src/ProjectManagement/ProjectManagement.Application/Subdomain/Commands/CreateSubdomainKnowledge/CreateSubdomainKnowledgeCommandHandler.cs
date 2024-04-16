using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Project.ValueObjects;
using ProjectManagement.Domain.Subdomain.Entities;

namespace ProjectManagement.Application.Subdomain.Commands.CreateSubdomainKnowledge;

public class CreateSubdomainKnowledgeCommandHandler
    : ICommandHandler<CreateSubdomainKnowledgeCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISubdomainRepository _subdomainRepository;

    public CreateSubdomainKnowledgeCommandHandler(ISubdomainRepository subdomainRepository, IUnitOfWork unitOfWork)
    {
        _subdomainRepository = subdomainRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateSubdomainKnowledgeCommand request, CancellationToken cancellationToken)
    {
        var subdomain = await _subdomainRepository.GetSubdomainById(SubdomainId.Create(request.SubdomainId), cancellationToken);

        if (subdomain.Value is null)
            return subdomain;

        subdomain.Value
            .AddKnowledge(SubdomainKnowledge
                    .Create(request.Title, request.Content, SubdomainId.Create(request.SubdomainId)));

        return await _unitOfWork
            .SaveChangesAsync(subdomain.Value.DomainEvents, cancellationToken);
    }
}
