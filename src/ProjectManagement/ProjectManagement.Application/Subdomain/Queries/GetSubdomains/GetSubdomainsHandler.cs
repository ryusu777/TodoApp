using Library.Models;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Assignment;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Subdomain.Queries.GetSubdomains;

public class GetSubdomainsHandler : IQueryHandler<GetSubdomainsQuery, GetSubdomainsResult>
{
    private readonly ISubdomainRepository _subdomainRepo;
    private readonly IAssignmentRepository _assignmentRepo;

    public GetSubdomainsHandler(ISubdomainRepository subdomainRepo, IAssignmentRepository assignmentRepo)
    {
        _subdomainRepo = subdomainRepo;
        _assignmentRepo = assignmentRepo;
    }

    public async Task<Result<GetSubdomainsResult>> Handle(GetSubdomainsQuery request, CancellationToken cancellationToken)
    {
        var result = await _subdomainRepo
            .GetSubdomains(ProjectId.Create(request.ProjectId), cancellationToken);

        if (result.IsFailure || result.Value is null)
            return Result.Failure<GetSubdomainsResult>(result.Error);

        var assignmentCountResult = await _assignmentRepo
            .GetOpenedAssignmentCountPerSubdomain(
                result
                    .Value
                    .Select(e => e.Id),
                cancellationToken);

        return Result
            .Success(new GetSubdomainsResult(result
                .Value
                !.Select(e => {
                    var result = Subdomain.Dtos.Subdomain.FromDomain(e);
                    result.NumOfOpenedAssignments = assignmentCountResult
                        .Value
                        ?.FirstOrDefault(a => a.SubdomainId == e.Id.Value)
                        ?.OpenedAssignmentCount ?? 0;
                    return result;
                })
                .ToList()));
    }
}
