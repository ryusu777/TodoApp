using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Project.Queries.GetProjectDashboard;

public record GetProjectDashboardQuery() : IQuery<GetProjectDashboardResult>;
