using Library.Models;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Subdomain.Queries.GetSubdomain;

public class GetSubdomainHandler : IQueryHandler<GetSubdomainQuery, GetSubdomainResult>
{
    private readonly ISubdomainRepository _subdomainRepo;

    public GetSubdomainHandler(ISubdomainRepository subdomainRepo)
    {
        _subdomainRepo = subdomainRepo;
    }

    public async Task<Result<GetSubdomainResult>> Handle(GetSubdomainQuery request, CancellationToken cancellationToken)
    {
        var result = await _subdomainRepo
            .GetSubdomainById(SubdomainId.Create(request.SubdomainId), cancellationToken);

        if (result.Error is not null)
        {
            return Result.Failure<GetSubdomainResult>(result.Error);
        }

        var subdomain = result.Value!;

        return Result.Success(new GetSubdomainResult(
            subdomain.Id.Value,
            subdomain.Description,
            subdomain.Title,
            subdomain.ProjectId.Value,
            subdomain.Knowledges
                .Select(e => Subdomain.Dtos.SubdomainKnowledge.FromDomain(e))
                .ToList()
        ));
    }
}
