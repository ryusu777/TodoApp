using Library.Models;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Application.Assignment;
using ProjectManagement.Domain.Assignment.Enums;

namespace ProjectManagement.Application.Project.Queries.GetProjectDashboard;

public class GetProjectDashboardHandler : IQueryHandler<GetProjectDashboardQuery, GetProjectDashboardResult>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IAssignmentRepository _assignmentRepository;

    public GetProjectDashboardHandler(IProjectRepository projectRepository, IAssignmentRepository assignmentRepository)
    {
        _projectRepository = projectRepository;
        _assignmentRepository = assignmentRepository;
    }

    public async Task<Result<GetProjectDashboardResult>> Handle(GetProjectDashboardQuery request, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetActiveProjects(cancellationToken);

        if (projects.IsFailure || projects.Value is null)
        {
            return Result.Failure<GetProjectDashboardResult>(projects.Error);
        }

        var result = new List<ProjectItem>(projects.Value.Count);
        foreach (var p in projects.Value)
        {
            var assignments = await _assignmentRepository.GetAssignments(p.Id, cancellationToken);

            result.Add(new ProjectItem(
                p.Id.Value,
                p.Name,
                p.Description,
                assignments.Value!.Count(a => a.Status == AssignmentStatusEnum.New.ToString() || a.Status == AssignmentStatusEnum.Revised.ToString()),
                assignments.Value!.Count(a => a.Status == AssignmentStatusEnum.WaitingReview.ToString()),
                assignments.Value!.Count(a => a.Status == AssignmentStatusEnum.OnProgress.ToString()),
                assignments.Value!.Count(a => a.Status == AssignmentStatusEnum.Completed.ToString())
            ));
        }

        return Result.Success(new GetProjectDashboardResult(result));
    }
}

