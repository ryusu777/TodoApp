using Library.Models;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Subdomain.Queries.GetSubdomains;

public class GetSubdomainsHandler : IQueryHandler<GetSubdomainsQuery, GetSubdomainsResult>
{
    private readonly ISubdomainRepository _subdomainRepo;

    public GetSubdomainsHandler(ISubdomainRepository subdomainRepo)
    {
        _subdomainRepo = subdomainRepo;
    }

    public async Task<Result<GetSubdomainsResult>> Handle(GetSubdomainsQuery request, CancellationToken cancellationToken)
    {
        var result = await _subdomainRepo
            .GetSubdomains(ProjectId.Create(request.ProjectId), cancellationToken);

        if (result.Error != Error.None)
            return Result.Failure<GetSubdomainsResult>(result.Error);

        return Result
            .Success(new GetSubdomainsResult(result
                .Value
                !.Select(e => Subdomain.Dtos.Subdomain.FromDomain(e))
                .ToList()));
    }
}
