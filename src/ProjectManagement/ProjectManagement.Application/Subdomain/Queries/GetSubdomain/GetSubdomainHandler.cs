using Library.Models;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Assignment;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Subdomain.Queries.GetSubdomain;

public class GetSubdomainHandler : IQueryHandler<GetSubdomainQuery, GetSubdomainResult>
{
    private readonly ISubdomainRepository _subdomainRepo;
    private readonly IAssignmentRepository _assignmentRepo;

    public GetSubdomainHandler(ISubdomainRepository subdomainRepo, IAssignmentRepository assignmentRepo)
    {
        _subdomainRepo = subdomainRepo;
        _assignmentRepo = assignmentRepo;
    }

    public async Task<Result<GetSubdomainResult>> Handle(GetSubdomainQuery request, CancellationToken cancellationToken)
    {
        var result = await _subdomainRepo
            .GetSubdomainById(SubdomainId.Create(request.SubdomainId), cancellationToken);

        if (result.IsFailure || result.Value is null)
        {
            return Result.Failure<GetSubdomainResult>(result.Error);
        }

        var assignmentCountResult = await _assignmentRepo
            .GetOpenedAssignmentsCountBySubdomain(SubdomainId.Create(request.SubdomainId), cancellationToken);

        var subdomain = result.Value!;

        return Result.Success(new GetSubdomainResult(
            subdomain.Id.Value,
            subdomain.Description,
            subdomain.Title,
            subdomain.ProjectId.Value,
            subdomain.Knowledges
                .Select(e => Subdomain.Dtos.SubdomainKnowledge.FromDomain(e))
                .ToList()
        ) { 
            NumOfOpenedAssignments = assignmentCountResult.Value
        });
    }
}
