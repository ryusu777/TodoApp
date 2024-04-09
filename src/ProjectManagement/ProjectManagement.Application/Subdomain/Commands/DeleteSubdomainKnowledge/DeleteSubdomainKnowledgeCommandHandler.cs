namespace ProjectManagement.Application.Subdomain.Commands.DeleteSubdomainKnowledge;

using System.Threading;
using System.Threading.Tasks;
using Library.Models;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Project.ValueObjects;

public class DeleteSubdomainKnowledgeCommandHandler
    : ICommandHandler<DeleteSubdomainKnowledgeCommand>
{
    private readonly ISubdomainRepository _subdomainRepo;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSubdomainKnowledgeCommandHandler(IUnitOfWork unitOfWork, ISubdomainRepository subdomainRepo)
    {
        _unitOfWork = unitOfWork;
        _subdomainRepo = subdomainRepo;
    }

    public async Task<Result> Handle(DeleteSubdomainKnowledgeCommand request, CancellationToken cancellationToken)
    {
        var result = await _subdomainRepo
            .GetSubdomainById(SubdomainId.Create(request.SubdomainId));

        if (result.Value is null)
            return result;

        result.Value.DeleteKnowledge(SubdomainKnowledgeId.Create(request.SubdomainKnowledgeId));

        return await _unitOfWork
            .SaveChangesAsync(result.Value.DomainEvents, cancellationToken);
    }
}



