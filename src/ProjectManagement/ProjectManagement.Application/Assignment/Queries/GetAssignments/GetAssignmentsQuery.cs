using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Queries.GetAssignments;

public record GetAssignmentsQuery(string ProjectId, Guid? SubdomainId) : IQuery<GetAssignmentsResult>;
